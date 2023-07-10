using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Summary:
// The 'IClonable' interface defines a contract for cloning objects.
// It provides a method 'Clone' that returns a cloned instance of the implementing type.
// The type parameter 'T' represents the type of object that can be cloned.
public interface IClonable<T>
{
	#region Methods

	// Summary:
	// Creates and returns a cloned instance of the implementing type.
	// The returned object should be an independent copy of the original object.
	// Returns:
	// The cloned instance of the implementing type.
	T Clone();

	#endregion
}

