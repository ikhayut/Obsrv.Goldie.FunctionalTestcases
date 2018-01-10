using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obsrv.Goldie.FunctionalTestcases
{
    class ExpectedResult
    {
        public long org_id { get; set; }
        public string tnum { get; set; }
        public long cust_id { get; set; }
        public long cust_billto_id { get; set; }
        public long cust_shipto_id { get; set; }
        public double freight { get; set; }
        public double tax { get; set; }
        public double total { get; set; }
        public string cust_name { get; set; }
        public double status { get; set; }
        public DateTime so_date { get; set; }
        public DateTime inv_date { get; set; }
        public DateTime due_date { get; set; }
        public string terms { get; set; }
        public long sp_id { get; set; }
        public string cashsale { get; set; }
        public string sp_name { get; set; }
        public double term_id { get; set; }
        public string shipping_method { get; set; }
        public DateTime ship_date { get; set; }
        public string cust_po_no { get; set; }
        public string flex_value { get; set; }
        public string hold_at_station { get; set; }
        public string card_type { get; set; }
        public string shipment_source { get; set; }
        public string bill_method { get; set; }
    }
}

    

