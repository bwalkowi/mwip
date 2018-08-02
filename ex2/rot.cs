using Quantum;
using Quantum.Operations;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace QuantumConsole
{
	public class QuantumTest
	{
		public static void Main()
		{
			QuantumComputer comp = QuantumComputer.GetInstance();
			Register x = comp.NewRegister(1, 2);
			x.Hadamard(0);
			x.RotateY(1.0471975511966, 1);
			x.CNot(target: 0, control: 1);
		}
	}
}
