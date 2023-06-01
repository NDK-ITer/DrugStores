namespace DrugStore.Areas.Admin.Data
{
    public class UserInput
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Idrole { get; set; }
        public DateTime? UpdateDate { get; set; }


        public bool Userlock { get; set; }

    }
}
