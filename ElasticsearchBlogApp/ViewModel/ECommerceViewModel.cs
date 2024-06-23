﻿using ElasticsearchBlogApp.Models;
using System.Text.Json.Serialization;

namespace ElasticsearchBlogApp.ViewModel
{
    public class ECommerceViewModel
    {
       
        public string Id { get; set; }
        
        public string CustomerFirstName { get; set; } = null!;
     
        public string CustomerLastName { get; set; } = null!;
      
        public string CustomerFullName { get; set; } = null!;
  
        public string Category { get; set; } = null!;
    
        public int OrderId { get; set; }
    
        public string OrderDate { get; set; }
       
        
        public double TaxfulTotalPrice { get; set; }
   
        public string Gender { get; set; } = null!;
    }
}
