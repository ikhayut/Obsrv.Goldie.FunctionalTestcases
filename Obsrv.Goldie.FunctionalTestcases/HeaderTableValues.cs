using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace Obsrv.Goldie.FunctionalTestcases
{
    class HeaderTableValues
    {
        [TableName("so_hdr")]
        public class so_hdr
        {
            // public long id { get; set; }
            public long org_id { get; set; }
            public string tnum { get; set; }
            public long cust_id { get; set; }
            public long cust_billto_id { get; set; }
            public long cust_shipto_id { get; set; }
            public double freight { get; set; }
            public float tax { get; set; }
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

          
            public static so_hdr FindbyPO(string cust_po_no)
            {
                
                using (var db = new Database("DATA SOURCE= fintst; User ID=idt_oasis; Password=idt_oasis_joe1;", "Oracle.ManagedDataAccess.Client"))
                {
                    return db.SingleOrDefault<so_hdr>(String.Format("select * from so_hdr where cust_po_no='{0}'", cust_po_no));
                   
                }
            }
            
        }

        }
}
