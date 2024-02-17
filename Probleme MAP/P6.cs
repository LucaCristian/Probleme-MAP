using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_MAP
{
    internal class P6
    {
        static void InsertNumberUtil(int[] arr, int num, int low, int high)
        {
            if (low > high)
            {
                Console.WriteLine($"Pozitia pe care se poate adauga este {low}");
                return;
            }

            int mid = (low + high) / 2;

            if (num < arr[mid])
            {
                InsertNumberUtil(arr, num, low, mid - 1);
            }
            else if (num > arr[mid])
            {
                InsertNumberUtil(arr, num, mid + 1, high);
            }
            else
            {
                arr[mid] = num;
            }
        }
    }
}
