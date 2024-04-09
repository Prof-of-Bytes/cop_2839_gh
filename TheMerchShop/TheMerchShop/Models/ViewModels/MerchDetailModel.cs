namespace TheMerchShop.Models
{
    public class MerchDetailModel
    {
        public Merch Merch { get; set; } 
        public List<Vendor> Vendors { get; set; } = new List<Vendor>();

        public List<string> VendorNames()
        {

           List<string> names = new List<string>();

           foreach (Vendor vendor in Vendors)
            {
                names.Add(vendor.Name);
            }

           return names;
        }
    }
}
