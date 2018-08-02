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
			Register y = comp.NewRegister(0, 2);
			y.Hadamard(1);
			y.CNot(target: 0, control: 1);
			comp.CNot(target: y[1], control: x[0]);
			comp.SigmaZ(y[1], x[1]);
			y.CNot(target: 0, control: 1);
			y.Hadamard(1);
		}
	}
}
