using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using X.PagedList;
using X.PagedList.Mvc;
using Microsoft.AspNetCore.Identity;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using DrugStore.Mail;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection.Metadata;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DrugStore.Others.Momo;
using DrugStore.Models;
using DrugStore.Services;

namespace DrugStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        List<GioHang> gioHangs;
        List<CT_HoaDon> cT_HoaDons;
        private HoaDon hoaDon;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor contx;
        private UserManager<AppNetUser> userManager;
        private SignInManager<AppNetUser> signInManager;
        private readonly IVnPayService vnPayService;


        public ShopController(IVnPayService vnPayService, UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            this.emailSender = emailSender;
            this.vnPayService = vnPayService;
            //var user = userManager.Users.ToList();
        }

        public List<SanPham> AutoConsulting(string consultString)
        {
            string[] temp = consultString.Trim().ToUpper().Split(' ');
            List<string> worldList = temp.ToList();
            List<SanPham> dsSP = dbContext.SanPhams.ToList();
            foreach (string world in worldList.ToList())
            {
                if (string.IsNullOrEmpty(world))
                {
                    break;
                }
                foreach (var item in dsSP.ToList())
                {
                    if (item.CongDung == null)
                    {
                        continue;
                    }
                    if (!item.CongDung.ToUpper().Contains(world))
                    {
                        dsSP.Remove(item);
                    }
                }
                if (dsSP.IsNullOrEmpty())
                {
                    dsSP = dbContext.SanPhams.ToList();
                }
                worldList.Remove(world);
            }
            if (dsSP.Count <= 0 || dsSP.Count == dbContext.SanPhams.ToList().Count)
            {
                return new List<SanPham>();
            }
            return dsSP;
        }

        public IActionResult Index(int? page, List<SanPham> sanPhams, string? consultString, string? idLoaiSP, Guid? idTHLSP, string? search)
        {
            int pageSize = 6;
            if (idLoaiSP != null)
            {
                sanPhams = dbContext.SanPhams.Where(s => s.MaLoaiSP == idLoaiSP).ToList();
            }
            if (sanPhams == null || sanPhams.Count <= 0)
            {
                sanPhams = dbContext.SanPhams.Where(s => s.MaTT == 1).OrderBy(s => s.TenSP).ToList();
            }
            if (idTHLSP != null)
            {
                sanPhams = dbContext.SanPhams.Where(c => c.Thuoc != null && c.Thuoc.MaLT == idTHLSP).ToList();
                if (sanPhams.IsNullOrEmpty())
                {
                    sanPhams = new List<SanPham>();
                }
            }
            if (search != null)
            {
                sanPhams = dbContext.SanPhams.Where(c => c.TenSP.Contains(search)).ToList();
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;
                return View(sanPhams.ToPagedList((int)page, pageSize));
            }
            if (consultString != null)
            {
                sanPhams = AutoConsulting(consultString);
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;

                return View(sanPhams.ToPagedList((int)page, pageSize));
            }
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            return View(sanPhams.ToPagedList((int)page, pageSize));
        }

        //public IActionResult FillLoaiThuoc(Guid id)
        //{
        //    List<SanPham> dssp = dbContext.SanPhams.Where(c => c.Thuoc.MaLT == id).ToList();
        //    return RedirectToAction("Index","Shop", dssp,null);
        //}

        public IActionResult Product(Guid id)
        {
            SanPham sanPham = dbContext.SanPhams.Find(id);
            if (signInManager.IsSignedIn(User) && (sanPham.Thuoc != null))
            {
                CT_CaNhanHoa cT_CaNhanHoa = dbContext.CT_CaNhanHoas.FirstOrDefault(c => c.Id == userManager.GetUserId(User) && c.MaTHLSP == sanPham.Thuoc.MaLT);
                if (cT_CaNhanHoa != null)
                {
                    cT_CaNhanHoa.SoLanXem++;
                    dbContext.SaveChanges();
                }
                else
                {
                    cT_CaNhanHoa = new CT_CaNhanHoa();
                    cT_CaNhanHoa.MaTHLSP = (Guid)sanPham.Thuoc.MaLT;
                    cT_CaNhanHoa.SoLanXem = 1;
                    cT_CaNhanHoa.Id = userManager.GetUserId(User);
                    dbContext.CT_CaNhanHoas.Add(cT_CaNhanHoa);
                    dbContext.SaveChanges();
                }
            }
            return View(sanPham);
        }

        public void TakeShopingCart(string idUser)
        {

            gioHangs = dbContext.GioHangs.Where(s => s.Id == idUser).ToList();
            if (gioHangs == null)
            {
                gioHangs = new List<GioHang>();
            }
        }

        private int CountCart()
        {
            TakeShopingCart(userManager.GetUserId(User));
            int result = 0;
            if (gioHangs.Count == 0)
            {
                return result;
            }
            foreach (var item in gioHangs)
            {
                result = result + (int)item.SoLuong;
            }
            return result;
        }

        private double SumPriceCart()
        {
            TakeShopingCart(userManager.GetUserId(User));
            double result = 0;
            if (gioHangs.Count == 0)
            {
                return result;
            }
            foreach (var item in gioHangs)
            {
                SanPham sanPham = dbContext.SanPhams.Find(item.MaSP);
                //if (doiTuongKD.GiamGia == 0)
                //{
                //    result = (double)((int)item.SoLuong * (doiTuongKD.DonGia)) + result;
                //    continue;
                //}
                result = (double)((int)item.SoLuong * (sanPham.DonGia - (sanPham.DonGia * sanPham.GiamGia / 100))) + result;
            }
            return result;
        }

        [Authorize]
        public ActionResult AddToCart(Guid id, string x)
        {
            TakeShopingCart(userManager.GetUserId(User));
            GioHang spGioHang = gioHangs.FirstOrDefault(n => n.MaSP == id);
            if (spGioHang != null)
            {
                spGioHang.SoLuong++;
                spGioHang.ThanhTien = spGioHang.ThanhTien + (spGioHang.SanPham.DonGia - (spGioHang.SanPham.DonGia * spGioHang.SanPham.GiamGia / 100));
                dbContext.GioHangs.Update(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(userManager.GetUserId(User));

            }
            else if (spGioHang == null)
            {
                SanPham sp = dbContext.SanPhams.FirstOrDefault(n => n.MaSP == id);
                spGioHang = new GioHang();
                spGioHang.MaSP = id;
                spGioHang.Id = userManager.GetUserId(User);
                spGioHang.SoLuong = 1;
                spGioHang.ThanhTien = (sp.DonGia - (sp.DonGia * sp.GiamGia / 100)) * spGioHang.SoLuong;
                dbContext.GioHangs.Add(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(userManager.GetUserId(User));

            }
            //if (!signInManager.IsSignedIn(User) && url != null)
            //{
            //    return Redirect(url);
            //}
            return Redirect(x);


        }

        [Authorize]
        public ActionResult DeleteFromCart(Guid id)
        {
            TakeShopingCart(userManager.GetUserId(User));
            GioHang sanPham = gioHangs.FirstOrDefault(n => n.MaSP == id);
            if (sanPham != null)
            {
                dbContext.GioHangs.Remove(sanPham);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Shopingcart");
        }

        [Authorize]
        public IActionResult Shopingcart(int page = 1)
        {
            TakeShopingCart(userManager.GetUserId(User));
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            ViewBag.CountCart = CountCart();
            ViewBag.SumPriceCart = SumPriceCart();
            return View(gioHangs.ToPagedList((int)page, (int)pageSize));
        }
        public IActionResult Pay()
        {
            TakeBill();
            cT_HoaDons = TakeListProductIsBougth();

            if (cT_HoaDons != null)
            {
                foreach (var item in cT_HoaDons)
                {
                    item.SanPham = dbContext.SanPhams.Find(item.MaSP);
                }
                hoaDon.CT_HoaDon = cT_HoaDons;
                ViewBag.CountProductBought = CountProductBought();
                ViewBag.SumProductBought = SumProductBought();
                ViewBag.HinhThucThanhToan = new SelectList(dbContext.HinhThucThanhToans, "MaHT", "TenHT");

            }

            return View(hoaDon);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(HoaDon hoaDon)
        {


            hoaDon.CT_HoaDon = TakeListProductIsBougth();
            hoaDon.TongThanhTien = (decimal)SumProductBought();
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.DaThanhToan = false;
            if (signInManager.IsSignedIn(User))
            {
                hoaDon.Id = userManager.GetUserId(User);
            }
            if (!ModelState.IsValid)
            {
                TakeBill();
                cT_HoaDons = TakeListProductIsBougth();

                if (cT_HoaDons != null)
                {
                    foreach (var item in cT_HoaDons)
                    {
                        item.SanPham = dbContext.SanPhams.Find(item.MaSP);
                    }
                    hoaDon.CT_HoaDon = cT_HoaDons;
                    ViewBag.CountProductBought = CountProductBought();
                    ViewBag.SumProductBought = SumProductBought();
                    ViewBag.HinhThucThanhToan = new SelectList(dbContext.HinhThucThanhToans, "MaHT", "TenHT");
                }

                return View(hoaDon);

            }
            SaveBill(hoaDon);


            if (hoaDon.HinhThucThanhToan.MaHT == 1)
            {
                return RedirectToAction("Momo", "Shop", hoaDon);
            }
            else if (hoaDon.HinhThucThanhToan.MaHT == 3)
            {

                return RedirectToAction("VnPay", "Shop", hoaDon);
            }
            else
            {
                SendMail(hoaDon);
            }
            contx.HttpContext.Session.Remove("dsSpMua");
            return RedirectToAction("Index");

        }
        public async void SendMail(HoaDon hoaDon)
        {
            var url = "https://localhost:7254/Mail/MailPay/Index/" + hoaDon.SoDH;

            using (HttpClient client1 = new HttpClient())
            {
                try
                {
                    // Send a GET request to the URL
                    HttpResponseMessage response = await client1.GetAsync(url);

                    // Ensure a successful response
                    response.EnsureSuccessStatusCode();

                    // Read the HTML content as a string
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    await emailSender.SendEmailAsync(hoaDon.Email, "Đơn Hàng", htmlContent);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void SaveBill(HoaDon hoaDon)
        {
            if (hoaDon != null)
            {
                List<CT_HoaDon> dsSpMua = hoaDon.CT_HoaDon.ToList();
                foreach (var item in dsSpMua)
                {
                    SanPham temp = dbContext.SanPhams.Find(item.MaSP);
                    temp.SoLanMua = item.SoLuong + temp.SoLanMua;
                    temp.SoLuong = temp.SoLuong - item.SoLuong;
                    if (temp.SoLanMua <= 0)
                    {
                        temp.MaTT = 2;
                    }
                    dbContext.SanPhams.Update(temp);
                }
                dbContext.HoaDons.Add(hoaDon);
                dbContext.SaveChanges();
            }
        }

        public void LoginPay(HoaDon hoaDon)
        {
            if (signInManager.IsSignedIn(User))
            {
                hoaDon.Id = userManager.GetUserId(User);
                AspNetUser aspNetUser = dbContext.AspNetUsers.Find(userManager.GetUserId(User));
                hoaDon.TenNguoiMua = aspNetUser.FirstName+" " + aspNetUser.LastName;
                hoaDon.Email = aspNetUser.Email;
                hoaDon.SDT = aspNetUser.PhoneNumber;
            }
            else
            {
                hoaDon.Id = null;
                hoaDon.TenNguoiMua = null;
                hoaDon.Email = null;
                hoaDon.SDT = null;
            }
        }

        public void TakeBill()
        {
            if (hoaDon == null)
            {
                hoaDon = new HoaDon();
                hoaDon.SoDH = Guid.NewGuid();
                hoaDon.NgayLap = DateTime.Now;
                hoaDon.DaThanhToan = false;
            }
            LoginPay(hoaDon);
            // string hoaDonString = JsonConvert.SerializeObject(hoaDon);
            //contx.HttpContext.Session.SetString("dsSpMua", hoaDonString);
        }

        public List<CT_HoaDon> TakeListProductIsBougth()
        {
            //TakeBill();
            List<CT_HoaDon> cT_HoaDons = new List<CT_HoaDon>();
            string dsSpMuaString = contx.HttpContext.Session.GetString("dsSpMua");
            //if (signInManager.IsSignedIn(User))
            //{
            //    if (dsSpMuaString == null)
            //    {
            //        contx.HttpContext.Session.SetString("dsSpMua" + userManager.GetUserId(User), JsonConvert.SerializeObject(cT_HoaDons));
            //    }
            //    else
            //    {
            //        cT_HoaDons = JsonConvert.DeserializeObject<List<CT_HoaDon>>(dsSpMuaString);
            //    }
            //}
            //else
            //{
            //    if (dsSpMuaString == null)
            //    {
            //        contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));
            //    }
            //    else
            //    {
            //        cT_HoaDons = JsonConvert.DeserializeObject<List<CT_HoaDon>>(dsSpMuaString);
            //    }
            //}
            if (dsSpMuaString == null)
            {
                contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));
            }
            else
            {
                cT_HoaDons = JsonConvert.DeserializeObject<List<CT_HoaDon>>(dsSpMuaString);
            }

            return cT_HoaDons;

        }

        public IActionResult DeleteProductIsBought(Guid id)
        {
            cT_HoaDons = TakeListProductIsBougth();
            CT_HoaDon? cT_HoaDon = cT_HoaDons.FirstOrDefault(c => c.MaSP == id);
            if (cT_HoaDon != null)
            {
                cT_HoaDons.Remove(cT_HoaDon);
                contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));
            }
            return RedirectToAction("Pay", "Shop");
        }



        public void AddProductIsBought(Guid idSP, int? soLuong)
        {
            SanPham sp = dbContext.SanPhams.Find(idSP);
            cT_HoaDons = TakeListProductIsBougth();
            if (sp != null)
            {
                CT_HoaDon? spDuocMua = cT_HoaDons.FirstOrDefault(c => c.MaSP == idSP);
                if (spDuocMua == null)
                {
                    spDuocMua = new CT_HoaDon();
                    spDuocMua.MaSP = sp.MaSP;
                    if (soLuong == 0)
                    {
                        spDuocMua.SoLuong = 1;
                    }
                    else
                    {
                        spDuocMua.SoLuong = (int)soLuong;
                    }
                    spDuocMua.ThanhTien = (sp.DonGia - (sp.DonGia * sp.GiamGia / 100)) * spDuocMua.SoLuong;
                    cT_HoaDons.Add(spDuocMua);
                    contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));

                }
                else
                {
                    if (soLuong > 1)
                    {
                        spDuocMua.SoLuong += (int)soLuong;
                    }
                    else
                    {
                        spDuocMua.SoLuong++;
                    }
                    spDuocMua.ThanhTien = spDuocMua.ThanhTien + (sp.DonGia - (sp.DonGia * sp.GiamGia / 100));
                    contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));

                }

            }
        }

        public IActionResult RemoveProductIsBought(Guid idSP)
        {
            SanPham sp = dbContext.SanPhams.Find(idSP);
            cT_HoaDons = TakeListProductIsBougth();

            if (sp != null)
            {
                CT_HoaDon spDuocMua = cT_HoaDons.FirstOrDefault(c => c.MaSP == idSP);
                if (spDuocMua != null)
                {
                    cT_HoaDons.Remove(spDuocMua);
                }
                contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));

            }

            return RedirectToAction("Pay", "Shop");
        }

        [HttpPost]
        public IActionResult ProductIsBought(Guid idSP, int soLuong, string strURL)
        {
            if (soLuong <= 0)
            {
                return Redirect(strURL);
            }

            if (soLuong > (int)dbContext.SanPhams.Find(idSP).SoLuong)
            {
                return Redirect(strURL);
            }
            AddProductIsBought(idSP, soLuong);
            return RedirectToAction("Pay", "Shop");

        }

        public int CountProductBought()
        {
            cT_HoaDons = TakeListProductIsBougth();
            return cT_HoaDons.Count();
        }

        public double SumProductBought()
        {
            cT_HoaDons = TakeListProductIsBougth();
            return (double)cT_HoaDons.Sum(s => s.ThanhTien);
        }

        public IActionResult PayForShopingCart()
        {
            var id = userManager.GetUserId(User);
            List<GioHang> shopingCart = dbContext.GioHangs.Where(s => s.Id == id).ToList();
            cT_HoaDons = TakeListProductIsBougth();
            foreach (var item in shopingCart)
            {
                AddProductIsBought(item.MaSP, item.SoLuong);
            }
            return RedirectToAction("Pay", "Shop");
        }

        public IActionResult Momo(HoaDon hoaDon)
        {



            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = hoaDon.SoDH.ToString();
            string returnUrl = "https://localhost:7254/Shop/ConfirmPaymentClient";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test


            string amount = hoaDon.TongThanhTien.ToString();
            string orderid = hoaDon.SoDH.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
             //
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public IActionResult ConfirmPaymentClient(Result result)
        {

            string rMessage = result.message;
            Guid rOrderId = new Guid(result.orderId);
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            int code = Convert.ToInt32(rErrorCode);

            if (code == 0)
            {
                hoaDon = dbContext.HoaDons.Where(p => p.SoDH == rOrderId).FirstOrDefault();
                if (hoaDon != null)
                {
                    hoaDon.DaThanhToan = true;
                }
                dbContext.SaveChanges();
                SendMail(hoaDon);

                ViewBag.Message = "Thanh toán thành công hóa đơn ";
                contx.HttpContext.Session.Remove("dsSpMua");
            }

            else
            {
                ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
            }
            return View();
        }

        public IActionResult VnPay(HoaDon model)
        {

            var url = vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult ConfirmPaymentClientVnPay()
        {
            var response = vnPayService.PaymentExecute(Request.Query);

            PaymentResponseModel responseModel = response;

            if (responseModel != null)
            {
                if (responseModel.Success)
                {
                    Guid rOrderId = new Guid(responseModel.OrderId);
                    hoaDon = dbContext.HoaDons.Where(p => p.SoDH == rOrderId).FirstOrDefault();
                    if (hoaDon != null)
                    {
                        hoaDon.DaThanhToan = true;
                    }
                    dbContext.SaveChanges();
                    SendMail(hoaDon);

                    ViewBag.Message = "Thanh toán thành công hóa đơn ";
                    contx.HttpContext.Session.Remove("dsSpMua");
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }

    }
}
