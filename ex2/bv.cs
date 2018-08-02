using Quantum;
using Quantum.Operations;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace QuantumConsole
{
	public class QuantumTest
	{
		static int size = 3;
		static QuantumComputer comp = QuantumComputer.GetInstance();

		public static void Main(){
			Register x = comp.NewRegister(0, size);
			Register y = comp.NewRegister(1, 1);

			for(int i = 0; i < size; ++i)
				x.Hadamard(i);
			y.Hadamard(0);

			bv(x, y);

			for(int i = 0; i < size; ++i)
				x.Hadamard(i);
			y.Hadamard(0);
		}

		public static void bv(Register x, Register y){
			Register a = comp.NewRegister(5, size);
			comp.CNot(control: x[0], target: y[0]);
			comp.CNot(control: x[2], target: y[0]);
		}
	}
}
