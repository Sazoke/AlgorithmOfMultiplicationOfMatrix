using System;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static int p;
        static int q;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите р");
            p = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите q");
            q = int.Parse(Console.ReadLine());
            var matrixA = GetMatrix("matrixA.txt");
            var matrixB = GetMatrix("matrixB.txt");

            //var result = new int[matrixA.GetLength(0)][];
            //for (int i = 0; i < matrixA.GetLength(0); i++)
            //    result[i] = new int[matrixA.GetLength(0)];

            //for (int s = 0; s < q; s++)
            //{
            //    for (int i = 0; i < q; i++)
            //        for (int j = 0; j < q; j++)
            //        {
            //            var blocOfMatrixA = new int[4];
            //            for (int y = 0; y < p; y++)
            //                for (int u = 0; u < p; u++)
            //                    blocOfMatrixA[y * p + u] = matrixA[i * p + y][s * p + u];
            //            var blocOfMatrixB = new int[4];
            //            for (int y = 0; y < p; y++)
            //                for (int u = 0; u < p; u++)
            //                    blocOfMatrixB[y * p + u] = matrixB[s * p + y][j * p + u];
            //            var newBlock = GetMultiplicationOfMatrix(blocOfMatrixA, blocOfMatrixB);
            //            for (int y = 0; y < p; y++)
            //                for (int u = 0; u < p; u++)
            //                    result[i * p + y][j * p + u] += newBlock[y * p + u];
            //        }
            //}
            //PrintMatrix(result);

            var result = new int[matrixA.GetLength(0)][];
            for (int i = 0; i < matrixA.GetLength(0); i++)
                result[i] = new int[matrixA.GetLength(0)];

            PrintMatrix(matrixA);
            BaseSlideMatrixA(matrixA);
            PrintMatrix(matrixA);
            PrintMatrix(matrixB);
            BaseSlideMatrixB(matrixB);
            PrintMatrix(matrixB);
            for (int s = 0; s < q; s++)
            {
                for (int i = 0; i < q; i++)
                    for (int j = 0; j < q; j++)
                    {
                        var blocOfMatrixA = new int[4];
                        for (int y = 0; y < p; y++)
                            for (int u = 0; u < p; u++)
                                blocOfMatrixA[y * p + u] = matrixA[i * p + y][j * p + u];
                        var blocOfMatrixB = new int[4];
                        for (int y = 0; y < p; y++)
                            for (int u = 0; u < p; u++)
                                blocOfMatrixB[y * p + u] = matrixB[i * p + y][j * p + u];
                        var newBlock = GetMultiplicationOfMatrix(blocOfMatrixA, blocOfMatrixB);
                        for (int y = 0; y < p; y++)
                            for (int u = 0; u < p; u++)
                                result[i * p + y][j * p + u] += newBlock[y * p + u];
                        PrintMatrix(result);
                    }
                if (s == q - 1)
                    break;
                SlideMatrixA(matrixA);
                SlideMatrixB(matrixB);
            }
        }

        private static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i][j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        private static int[][] GetMatrix(string file)
        {
            using (var streamReader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + file, Encoding.Default)) 
            {
                var result = new int[p*q][];
                var line = "";
                var k = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var numbers = line.Split(' ');
                    var array = new int[numbers.Length];
                    for (int i = 0; i < numbers.Length; i++)
                        array[i] = int.Parse(numbers[i]);
                    result[k] = array;
                    k++;
                }
                return result;
            }
        }

        private static int[] GetMultiplicationOfMatrix(int[] numbersA, int[] numbersB)
        {
            var result = new int[numbersA.Length];
            for (int i = 0; i < numbersA.Length / p; i++)
                for (int j = 0; j < numbersB.Length / p; j++)
                    for (int k = 0; k < numbersB.Length / p; k++)
                        result[i * p + j] += numbersA[i * p + k] * numbersB[k * p + j];
            return result;
        }

        private static void BaseSlideMatrixA(int[][] matrixA)
        {
            for (int i = 0; i < matrixA.GetLength(0); i+=p)
            {
                var arr = new int[p][];
                for (int j = 0; j < p; j++)
                    arr[j] = new int[i];
                for (int j = 0; j < i; j++)
                    for (int k = 0; k < p; k++)
                        arr[k][j] = matrixA[i + k][j];
                for (int j = i; j < matrixA.GetLength(0); j++)
                    for (int k = 0; k < p; k++)
                        matrixA[i + k][j - i] = matrixA[i + k][j];
                for (int j = 0; j < arr[0].Length; j++)
                    for (int k = 0; k < p; k++)
                        matrixA[i + k][j + matrixA.GetLength(0) - arr[k].Length] = arr[k][j];
            }
        }

        private static void SlideMatrixA(int[][] matrixA)
        {
            for (int i = 0; i < matrixA.GetLength(0); i += p)
            {
                var arr = new int[p][];
                for (int j = 0; j < p; j++)
                    arr[j] = new int[p];
                for (int j = 0; j < p; j++)
                    for (int k = 0; k < p; k++)
                        arr[k][j] = matrixA[i + k][j];
                for (int j = p; j < matrixA.GetLength(0); j++)
                    for (int k = 0; k < p; k++)
                        matrixA[i + k][j - p] = matrixA[i + k][j];
                for (int j = 0; j < arr[0].Length; j++)
                    for (int k = 0; k < p; k++)
                        matrixA[i + k][j + matrixA.GetLength(0) - arr[k].Length] = arr[k][j];
            }
        }

        private static void BaseSlideMatrixB(int[][] matrixB)
        {
            for (int i = 0; i < matrixB.GetLength(0); i+=p)
            {
                var arr = new int[p][];
                for (int j = 0; j < p; j++)
                    arr[j] = new int[i];
                for (int j = 0; j < i; j++)
                    for (int k = 0; k < p; k++)
                        arr[k][j] = matrixB[j][i + k];
                for (int j = i; j < matrixB.GetLength(0); j++)
                    for (int k = 0; k < p; k++)
                        matrixB[j - i][i + k] = matrixB[j][i + k];
                for (int j = 0; j < arr[0].Length; j++)
                    for (int k = 0; k < p; k++)
                        matrixB[j + matrixB.GetLength(0) - arr[0].Length][i + k] = arr[k][j];
            }
        }

        private static void SlideMatrixB(int[][] matrixB)
        {
            for (int i = 0; i < matrixB.GetLength(0); i += p)
            {
                var arr = new int[p][];
                for (int j = 0; j < p; j++)
                    arr[j] = new int[p];
                for (int j = 0; j < p; j++)
                    for (int k = 0; k < p; k++)
                        arr[k][j] = matrixB[j][i + k];
                for (int j = p; j < matrixB.GetLength(0); j++)
                    for (int k = 0; k < p; k++)
                        matrixB[j - p][i + k] = matrixB[j][i + k];
                for (int j = 0; j < arr[0].Length; j++)
                    for (int k = 0; k < p; k++)
                        matrixB[j + matrixB.GetLength(0) - arr[0].Length][i + k] = arr[k][j];
            }
        }
    }
}
