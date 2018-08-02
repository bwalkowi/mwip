using Quantum;
using Quantum.Operations;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace QuantumConsole{

	public class QuantumTest{
		static QuantumComputer comp = QuantumComputer.GetInstance();

		static int p = 5;
		static int q = 11;
   		static int N = p * q;

		static Random rand = new Random();
		static int c = 17;

		static int a = 9;
		static int b = exp_mod(a, c, N);


		public static void Main(){
			Console.WriteLine("p = {0}", p);
			Console.WriteLine("q = {0}", q);
			Console.WriteLine("c = {0}", c);
			Console.WriteLine("N = {0}", N);
			Console.WriteLine("a = {0}", a);
			Console.WriteLine("b = {0}", b);
			Console.WriteLine();

//			int period = FindPeriod(N, a);
//			Console.WriteLine();
//			Console.WriteLine("period = {0}", period);			

			ex4();
		}

		static void ex1(){
			for(int x = 1; x <= 100; ++x)
		        Console.WriteLine ("Dla {0} reszta to {1}", x, exp_mod(a, x, N));
		}

		static void ex2(){
			for(int x = 1; x <= 100; ++x)
		        Console.WriteLine ("Dla {0} reszta to {1}", x, qu_exp_mod(a, x, N));
		}

		static void ex3(){
			int a_, p_, q_, r, x;
			while(true){
				a_ = rand.Next(2, N);
				if(gcd(a_, N) == a_){
					p_ = a_;
					q_ = N / a_;
					break;
				}

				r = FindPeriod(N, a_);
				if(r == -1 || r % 2 == 1)
					continue;				

				x = exp_mod(a_, r / 2, N);
				if((x + 1) % N != 0){
					p_ = gcd(N, x + 1);
					q_ = gcd(N, x - 1);
					break;
				}
			}			
			int d = inverse_modulo(c, (p_ - 1) * (q_ - 1));

			Console.WriteLine();
			Console.WriteLine("a_ = {0}, p_ = {1}, q_ = {2}", a_, p_, q_);			
			Console.WriteLine("d = {0}, b^d (mod N) = {1}", d, Math.Pow(b, d) % N);			
		}

		static void ex4(){
			int d = inverse_modulo(c, (p - 1) * (q - 1));
			Console.WriteLine("d = {0}, b^d (mod N) = {1}", d, Math.Pow(b, d) % N);
			Console.WriteLine();

			int r = FindPeriod(N, b);
			while(r == -1)
				r = FindPeriod(N, b);

			int d_prim = inverse_modulo(c, r);
			Console.WriteLine();
			Console.WriteLine("d' = {0}, b^d (mod N) = {1}", d_prim, Math.Pow(b, d_prim) % N);
		}

		static int gcd(int a, int b){
			if(a == 0)
				return b;

			return gcd(b % a, a);
		}

		static int inverse_modulo(int c, int mod){
			int[] r = {c, mod};
			int[] d = {1, 0};
			int[] k = {0, 1};
			int idx1 = 0, idx2 = 1;
			int ratio;
			while(r[idx1] != 1){
				ratio = (int)Math.Floor((double)r[idx1] / r[idx2]);
				r[idx1] -= ratio * r[idx2];
				d[idx1] -= ratio * d[idx2];
				k[idx1] -= ratio * k[idx2];
			
				idx1 = idx2;
				idx2 = 1 - idx1;
			}
			return d[idx1] > 0 ? d[idx1] : d[idx1] + mod;
		}

		static int exp_mod(int a, int x, int N){
			int tmp = a % N;
			int b = (x & 1) > 0 ? tmp : 1;
			for(int i = 1; i <= (int)Math.Ceiling(Math.Log(x, 2)); ++i){
				tmp = (tmp * tmp) % N;
				if((x & (1 << i)) > 0)
					b = (b * tmp) % N;
			}
			return b;
		}

		static int qu_exp_mod(int a, int x, int N){
	        // obliczamy ile bitow potrzeba na zapamiętanie N
			ulong ulongN = (ulong)N;
			int width = (int)Math.Ceiling(Math.Log(N, 2));
			
		 	//inicjalizujemy rejestr wejsciowy 	
			Register regX = comp.NewRegister(0, 2 * width);
			
			// inicjalizujemy rejestr wyjsciowy 	
			Register regY = comp.NewRegister(1, width + 1);

			// ustawiamy wartosc rejestru wejsciowego na x 
			regX.Reset((ulong)x);

			// ustawiamy wartosc rejestru wyjsciowego na 1
 	        // potrzebne, gdy wywolujemy w petli 
			regY.Reset(1);

	        // obliczamy a^x mod N
		 	comp.ExpModulo(regX, regY, a, N);

			//mierzymy wartosc
			return (int)regY.Measure();
		}

		public static Tuple<int, int> FractionalApproximation(int a, int b, int width){
            double f = (double)a / (double)b;
            double g = f;
            int i, num2 = 0, den2 = 1, num1 = 1, den1 = 0, num = 0, den = 0;
            int max = 1 << width;

            do{
                i = (int)g;  // integer part
                g = 1.0 / (g - i);  // reciprocal of the fractional part

                if (i * den1 + den2 > max) // if denominator is too big
                {
                    break;
                }

                // new numerator and denominator
                num = i * num1 + num2;
                den = i * den1 + den2;

                // previous nominators and denominators are memorized
                num2 = num1;
                den2 = den1;
                num1 = num;
                den1 = den;

            }while (Math.Abs(((double)num / (double)den) - f) > 1.0 / (2 * max));
            // this condition is from Shor algorithm

            return new Tuple<int, int>(num, den);
        }

		public static int FindPeriod(int N, int a) {
			ulong ulongN = (ulong)N;
			int width = (int)Math.Ceiling(Math.Log(N, 2));
 
//			Console.WriteLine("Width for N: {0}", width);
//			Console.WriteLine("Total register width (7 * w + 2) : {0}", 7 * width + 2);
			
			QuantumComputer comp = QuantumComputer.GetInstance();
			
			//input register
			Register regX = comp.NewRegister(0, 2 * width);
			
			// output register (must contain 1):
			Register regX1 = comp.NewRegister(1, width + 1);
			
			// perform Walsh-Hadamard transform on the input register
			// input register can contains N^2 so it is 2*width long
//			Console.WriteLine("Applying Walsh-Hadamard transform on the input register...");
			comp.Walsh(regX);
			
			// perform exp_mod_N
//			Console.WriteLine("Applying f(x) = a^x mod N ...");
			comp.ExpModulo(regX, regX1, a, N);
			
			// output register is no longer needed
			regX1.Measure();
			
			// perform Quantum Fourier Transform on the input register
//			Console.WriteLine("Applying QFT on the input register...");
			comp.QFT(regX);
			
			comp.Reverse(regX);
			
			// getting the input register
			int Q = (int)(1 << 2 * width);
			int inputMeasured = (int)regX.Measure();
//			Console.WriteLine("Input measured = {0}", inputMeasured);
//			Console.WriteLine("Q = {0}", Q);
			
			Tuple<int, int> result = FractionalApproximation(inputMeasured, Q, 2 * width - 1);
 
//			Console.WriteLine("Fractional approximation:  {0} / {1}", result.Item1, result.Item2);
			
			int period = result.Item2;

			if(BigInteger.ModPow(a, period, N) == 1) {
				Console.WriteLine("Success !!!    period = {0}", period);
				return period;
			}
			
			int maxMult = (int)(Math.Sqrt(N)) + 1;
			int mult = 2;
			while(mult < maxMult) 
			{
//				Console.WriteLine("Trying multiply by {0} ...", mult);
				period = result.Item2 * mult;
				if(BigInteger.ModPow(a, period, N) == 1) 
				{
					Console.WriteLine("Success !!!    period = {0}", period);
					return period;
				}
				else 
				{		
					mult++;
				}
			}

			Console.WriteLine("Failure !!!    Period not found, try again.");
			return -1;
		}

	}
}
