public static class IntExtensions{
     public static bool IsPrime(this int quantity)
        {
            if (quantity < 2) return false;
            for (int i = 2; i <= Math.Sqrt(quantity); i++)
            {
                if (quantity % i == 0) return false;
            }
            return true;
        }
}