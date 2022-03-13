﻿namespace Model.ViewModel
{
    public class Product_Brand_Color_Img
    {
        public long Id { get; set; }
        public string NamePro { get; set; }
        public string Description { get; set; }
        public string NameBrand { get; set; }
        public int IdBrand { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public string Option { get; set; }
        public int Type { get; set; }
        public decimal Warranty { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public string UrlImage { get; set; }
        public long IdImage { get; set; }

        public string Color { get; set; }
        public int IdColor { get; set; }
    }
}
