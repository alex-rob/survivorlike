[[General Programming Concepts]]

There are different kinds of exceptions that can be thrown.

ArgumentException is useful when an argument does not have a fulfilled or correct value
```
throw new ArgumentException(string, nameof(parameter))
```

InvalidOperationException is good for attempting access of an object when it is in an incorrect state, such as trying to write to a read-only file.
```
throw new InvalidOperationException(string)
```

ArgumentOutOfRangeException is typically used to catch a system index out of range exception and then to throw a new exception. 
```
try
{
	return array[index];
}
catch (IndexOutOfRangeException e)
{
	throw new ArgumentOutOfRangeException("Parameter index is out of range.", e)
}
```
**NOTE: It is best practice to check the range of the index *before* accessing the array rather than relying on catching an exception.**

