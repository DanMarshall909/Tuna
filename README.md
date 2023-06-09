# 🐟 Tuna 🐟

This is an attempt to make a generic dynamic, real-time non-linear optimisation engine - a function "tuna". Currently, it serves as a personal learning exercise and a platform to explore several ideas that have been lingering in my mind, rather than a fully-fledged project (which I'm sure has been tackled before).

## 🐟 Motivation 🐟

Imagine you're assessing the effectiveness of a database insert statement. While you can manipulate the batch size, there are external factors on the database side, such as load and memory, that are beyond your direct control. Consequently, the database's performance may fluctuate over time, requiring real-time adjustments to optimize it. It's important to note that modifying the batch size might yield different results than expected, necessitating ongoing experimentation to determine the ideal setting. Tuna, attempts to automate this process.

## 🐟 Inputs 🐟 

The inputs will be:
- The function to be optimized, represented as `(x) => f(x, t)`, where `x` is a parameter that can be varied, and `t` represents time in seconds.
	- the function may vary unpredictably over time.
	- `t` does not need to be explicitly provided (in fact it most likely won't)  
	- mult-valued functions will also be supported e.g. `(x, y) => f(x, y, t)`
	- the function can return any type, and it will be up to the ranking function to determine which output is more favourable.
	- a function will also need to be provided to determine how much the engine should vary this parameter every second. Helper functions will be available for this function such as:
		- Variation: `(difference(x1, x2), deltaT) => V(difference(x1, x2), deltaT)`, where 
			- `deltaT` is the number of seconds that you're checking variation over.
			- `difference(x1, x2)` is a function that provides a numeric value to be compared. The types of x1 and x2 are the same as the parameter.	
	- this function could simply be a constant such as 0.01 which will update 
- the parameters for that function e.g. `x` where `x` can be decimal, floating point, integer, string or enum.
- boolean functions for retraints on the parameters e.g. `(x) => x >= 10 && x < 1000` or `e => e != MyEnum.None`
- a ranking function to determine a score where a higher value is a more favourable result e.g. `(result) => result.SomeNumericValue + result.myEnum ? MyEnum.Best : 10 ? MyEnum.Fair : 5 : 0` 
