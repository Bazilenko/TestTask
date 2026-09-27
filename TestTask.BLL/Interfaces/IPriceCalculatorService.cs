namespace TestTask.BLL.Interfaces;
public interface IPriceCalculatorService
{
    decimal CalculateRoomPrice(decimal baseHourlyRate, DateTimeOffset startTime, DateTimeOffset endTime);
}