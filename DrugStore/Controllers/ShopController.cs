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


        public ShopController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            this.emailSender = emailSender;
            //var user = userManager.Users.ToList();
        }
        public IActionResult Index(int? page)
        {
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            return View(dbContext.SanPhams.OrderBy(s => s.TenSP).ToPagedList((int)page, pageSize));
        }
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
        public ActionResult AddToCart(Guid id, string strURL)
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
            return Redirect(strURL);


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
            else
            {
                SendMail(hoaDon);
            }
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
                hoaDon.TenNguoiMua = aspNetUser.FirstName + aspNetUser.LastName;
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
            string dsSpMuaString = contx.HttpContext.Session.GetString("dsSpMua");
            List<CT_HoaDon> cT_HoaDons = new List<CT_HoaDon>();
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

        public void AddProductIsBought(Guid idSP, int? soLuong)
        {
            SanPham sp = dbContext.SanPhams.Find(idSP);
            cT_HoaDons = TakeListProductIsBougth();

            if (sp != null)
            {
                CT_HoaDon spDuocMua = cT_HoaDons.FirstOrDefault(c => c.MaSP == idSP);
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
            string returnUrl = "https://localhost:7254/Home/ConfirmPaymentClient";
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
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            int code = Convert.ToInt32(rErrorCode);
            if (code == 0)
            {
                hoaDon = dbContext.HoaDons.Where(p => p.SoDH.Equals(result.orderId)).FirstOrDefault();
                if (hoaDon != null)
                {
                    hoaDon.DaThanhToan = true;
                }
                dbContext.SaveChanges();
                SendMail(hoaDon);
            }


            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
            string a = "";
        }
    }
}
