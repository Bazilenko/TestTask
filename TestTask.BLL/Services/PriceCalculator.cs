using TestTask.BLL.Interfaces;

namespace TestTask.BLL.Services;
public class PriceCalculatorService : IPriceCalculatorService
{
    public decimal CalculateRoomPrice(decimal baseHourlyRate, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        decimal totalPrice = 0;
        var currentTime = startTime;

        while (currentTime < endTime)
        {
            decimal multiplier = GetMultiplierForTime(currentTime.Hour);
            totalPrice += baseHourlyRate * multiplier;
            
            currentTime = currentTime.AddHours(1);
        }

        return totalPrice;
    }

    private decimal GetMultiplierForTime(int hour)
    {
        if (hour >= 6 && hour < 9) 
            return 0.90m;
        
        if (hour >= 12 && hour < 14) 
            return 1.15m;
        
        if (hour >= 18 && hour < 23) 
            return 0.80m;
        
        return 1.00m;
    }
}