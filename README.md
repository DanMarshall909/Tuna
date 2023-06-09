# 🐟 Tuna 🐟

This is an attempt to make a generic dynamic, real-time non-linear optimisation engine - a function "tuna". 

Currently, it serves as a personal learning exercise, challange and a platform to explore several ideas that have been lingering in my mind, rather than a fully-fledged project (which I'm sure has been tackled before).

## 🐟 Motivation 🐟

Imagine you're assessing the effectiveness of a database insert statement. While you can manipulate the batch size, there are external factors on the database side, such as load and memory, that are beyond your direct control. Consequently, the database's performance may fluctuate over time, requiring real-time adjustments to optimize it. It's important to note that modifying the batch size might yield different results than expected, necessitating ongoing experimentation to determine the ideal setting. Tuna, attempts to automate this process.

## 🐟 Inputs 🐟 

The inputs will be:
- The function to be optimized, represented as `(x) => f(x, t)`, where `x` is a parameter that can be varied, and `t` represents time.
	- the function may vary unpredictably over time (`t`).- `t` does not need to be explicitly provided (in fact it most likely won't)  
	- mult-valued functions will also be supported e.g. `(x, y) => f(x, y, t)`
	- the function can return any type, and it will be up to the ranking function to determine which output is more favourable.
- the parameters for that function e.g. `x` where `x` can be decimal, floating point, integer, string or enum.
- boolean functions for retraints on the parameters e.g. `(x) => x >= 10 && x < 1000` or `e => e != MyEnum.None`
- a ranking function to determine a score where a higher value is a more favourable result e.g. `(result) => result.SomeNumericValue + result.myEnum ? MyEnum.Best : 10 ? MyEnum.Fair : 5 : 0` 
- a function to determine how agressively the engine should explore other solutions e.g. ()
