// User story

// As an API client user I need to be able to:
// 	specify a parameter range
// 		Parameter ranges mean different things for different parameter types
// 			Numeric - various defined intervals (open or closed over the interval)
// 			Enums			
// 	specify hints for choosing certain parameters first when enumerating various combinations. Hints carry weights 
// 	specify options:
// 		timeouts
// 			hard time out. Always fail on this limit.
// 			adaptive timeout (if a specified number of timeouts occur then update the timeout)
// 			timeout options
// 				- retry indefinately
// 				- tune parameters after  
// 				- if a specified number of timeouts occur after tuning then increase the timeout limit by a designated percentage


API examples

var Tuna = new Tuna(new MyTaskRunner())
	.WithHardTimeout(500)
	.WithParameter<decimal>("Number of rows to insert")
		.WithInterval<decimal>(Interval.Open, 1m, Interval.Closed, 50m)
		.WithInterval<decimal>(Interval.Closed, 1000m, Interval.Closed, 500000m)
	.WithParameter<InsertMethod>("Insert method")
		.WithParameterOptions<InsertMethod>(InsertMethod.BulkInsert, InsertMethod.IndividualInserts);

var result = await Tuna.RunAsync();

....

class MyTaskRunner : AbstractTaskRunner()
{
	public override void Task()
	{
		var numRowsToInsert = GetParameter<decimal>("Number of rows to insert");
		var insertMethod = GetParameter<InsertMethod>("Insert method");

		var data = GetTheData();

		switch (insertMethod)
		{		
			InsertMethod.BulkInsert: DoBulkInsert(data, numRowsToInsert); break;
			InsertMethod.IndividualInserts: : DoIndividualInserts(data); break;
			default:
		}		
	}
}
