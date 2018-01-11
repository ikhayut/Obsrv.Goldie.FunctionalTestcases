Feature: File upload and data processing
	In order to process Goldie sales data correctly
	As an AR Specialist
	I want to be able to transfer Goldie data into IDT database

@FileUpload
Scenario Outline: File upload
	Given Test data deleted from DB tables prior to file upload
	When File import was run
	And Status in the log table = S
	Then Records with matching <cust_po_no> inserted into Header
	Examples:
	| cust_po_no |
	| 2585991    |
	| 2585992    |

	@SalesOrder
Scenario: Header record check
When SalesOrder 2585998 is imported
Then Heder record values should be as expected
| org_id | tnum    | cust_id | cust_billto_id | cust_shipto_id | freight | tax  | total  | cust_name         | status | so_date  | inv_date | due_date | terms       | sp_id     | cashsale | sp_name           | term_id | shipping_method | ship_date | cust_po_no | flex_value | hold_at_station | card_type | shipment_source | bill_method |
| 1407   | PENDING | 101648  | 539012         | 539012         | 14.00   | 9.38 | 323.38 | LA ESTRELLITA LLC | 3      | 8/7/2017 | 8/7/2017 | 8/7/2017 | CONSIGNMENT | 100004901 | Y        | CLAUDIA SALAMANCA | 2410    | GG              | 8/7/2017  | 2585998    | GG         | N               | CARD      | INHOUSE         | ACT         |
	

@SalesOrder
Scenario Outline: SalesOrder number of items check
	When File import was run
	Then Correct number of <lines> generated for each "<cust_po_no>"
	Examples:
	| cust_po_no | lines |
	| 2585991    | 1     |
	| 2585989    | 2     |
	| 2585993    | 2     |
	


	@SalesOrder
Scenario Outline: Freight value check
	When File import was run
	Then Freight calculated correctly based on item <line1>, <line2> for "<cust_po_no>"
	Examples:
	 | line1 | line2 | cust_po_no |
	 | 7.00  | 7.00  | 2585989    |
	 | 5.00  | 0.00  | 2585990    |
	

	@SalesOrder
Scenario Outline: Tax rate
	When File import was run
	Then Tax rate equals <taxamount> / <price> * <qty> for "<cust_po_no>" for <itemid>
	Examples:
	| taxamount | price  | qty | cust_po_no | itemid |
	| 4.62  | 150.99 | 7   | 2585989    | 512669 |
	|
	
	

	