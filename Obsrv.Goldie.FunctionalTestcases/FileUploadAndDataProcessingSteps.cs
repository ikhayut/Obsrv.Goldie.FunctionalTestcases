using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using NUnit.Framework;


namespace Obsrv.Goldie.FunctionalTestcases
{
    [Binding]
    public class FileUploadAndDataProcessingSteps
    {
        public static OracleConnection connection;
        [Given]
        public void Given_Test_data_deleted_from_DB_tables_prior_to_file_upload()
        {
            string connectionString = "DATA SOURCE= fintst; User ID=idt_oasis; Password=idt_oasis_joe1;";
            connection = new OracleConnection(connectionString);
            connection.Open();
            Debug.WriteLine("Connection to fintst: " + connection.State);
            OracleCommand command = connection.CreateCommand();
            string sql = "select count(*) from so_hdr where cust_po_no = '258599801'";
            command.CommandText = sql;
            OracleDataReader reader = command.ExecuteReader();
            int rc_count;
            while (reader.Read())
            {
                rc_count = reader.GetInt32(0);

                if (rc_count > 0)
                {
                    // reader.Close();
                    connection.Close();
                    connection.Dispose();
                    Assert.Fail("Please delete duplicates from so_hdr");

                }
                //   reader.Close();
            }

            // OracleCommand command1 = connection.CreateCommand();
            string sql1 = "select count(*) from so_dtl where hdr_id in (select id from so_hdr where cust_po_no = '258599801')";
            command.CommandText = sql1;
            OracleDataReader reader1 = command.ExecuteReader();
            int dtl_count;
            while (reader1.Read())
            {
                dtl_count = reader1.GetInt32(0);
                if (dtl_count > 0)
                {
                    //  reader.Close();
                    connection.Close();
                    connection.Dispose();

                    Assert.Fail("Please delete duplicates from so_dtl");

                }

            }
            //   reader.Close();
        }

    //}

    [When]
        public void When_File_import_was_run()
        {
            Console.WriteLine("placeholder"); //ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_Status_in_the_log_table_S()
        {
            OracleCommand command = connection.CreateCommand();
            string sql = "select status from file_upload_log_header where log_header_id = (select MAX(log_header_id) from file_upload_log_header)";
            command.CommandText = sql;
            OracleDataReader reader = command.ExecuteReader();
            //string status;
            while (reader.Read())
            {
                if (reader["STATUS"].Equals("F"))
                // var Status = reader["STATUS"].ToString();

                {

                   // connection.Close();
                   //connection.Dispose();
                    //Assert.Fail("Upload failed");

                }
                else if (reader["STATUS"].Equals("P"))
                {
                    Console.WriteLine("File upload successful");
                }
                else
                {
                    connection.Close();
                    connection.Dispose();

                    Assert.Fail("Upload failed");

                }
            }
        }
        
        [When]
        public void When_SalesOrder_PO_is_imported(string po)
        {
            Console.WriteLine("placeholder"); //ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_Records_with_matching_PO_inserted_into_Header(string po)
        {
          //  var count = HeaderTableValues.so_hdr.CheckPOExists(po);
           // Console.WriteLine("value {0}", count);

            OracleCommand command = connection.CreateCommand();
            string sql = (String.Format("select count(*) from so_hdr where cust_po_no='{0}'", po));
            command.CommandText = sql;
            OracleDataReader reader = command.ExecuteReader();
            int rc_count;
            while (reader.Read())
            {
                rc_count = reader.GetInt32(0);

                if (rc_count == 0)
                {
                     reader.Close();
                    
                    Assert.Fail("SalesOrder {0} does not exist in Header", po);

                }
                else if (rc_count >=1)
                {
                    reader.Close();

                    Assert.Pass("SalesOrder {0} exists in Header", po);
            
                    
                }
               //   reader.Close();
          }

        }

        [Then]
        public void Then_Correct_number_of_LINES_generated_for_each_PO(int lines, string po)
        {
            string connectionString = "DATA SOURCE= fintst; User ID=idt_oasis; Password=idt_oasis_joe1;";
            connection = new OracleConnection(connectionString);
            connection.Open();
            Debug.WriteLine("Connection to fintst: " + connection.State);
            OracleCommand command = connection.CreateCommand();
            // connection = new OracleConnection(connectionString);
            //connection.Open();
            //OracleCommand command = connection.CreateCommand();
             string sql = (String.Format("select count(*) from so_dtl where hdr_id in (select id from so_hdr where cust_po_no='{0}')", po));
             command.CommandText = sql;
            OracleDataReader reader = command.ExecuteReader();
            //OracleDataReader reader = command.ExecuteReader();
            int count;
           while (reader.Read())
            {
                 count = reader.GetInt32(0);
                 Assert.AreEqual(count, lines);
              //  reader.Close();
            }
            reader.Close();
            //// ScenarioContext.Current.Pending();
        }

        [Then]
        public void Then_Heder_record_values_should_be_as_expected(Table table)
        {
           
            var er = table.CreateInstance<ExpectedResult>();
            var db = HeaderTableValues.so_hdr.FindbyPO("2585998");
           // var hdr = table.CreateInstance<HeaderTableValues>();
            table.CompareToInstance<HeaderTableValues.so_hdr>(db);
           
        }
        [Then(@"Freight calculated correctly based on item (.*), (.*) for ""(.*)""")]
        public void ThenFreightCalculatedCorrectlyBasedOnItemFor(Decimal line1, Decimal line2, string po)
        {
            //ScenarioContext.Current.Pending();
            string connectionString = "DATA SOURCE= fintst; User ID=idt_oasis; Password=idt_oasis_joe1;";
            connection = new OracleConnection(connectionString);
            connection.Open();
            Debug.WriteLine("Connection to fintst: " + connection.State);
            OracleCommand command = connection.CreateCommand();
            string sql = (String.Format("select freight from so_hdr where cust_po_no ='{0}'", po));
            command.CommandText = sql;
            OracleDataReader reader = command.ExecuteReader();
            decimal freight = line1 + line2;
            while (reader.Read())
            {
                Assert.AreEqual(reader["FREIGHT"], freight);

            }
            reader.Close();
        }
        [Then]
        public void Then_tax_rate_equals_TAXAMT_PRICE_QTY_for_PO(Decimal taxamt, Decimal price, int qty, string po)
        {
            ScenarioContext.Current.Pending();
        }



    }
}
