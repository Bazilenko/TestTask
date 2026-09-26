using System.Linq.Expressions;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Specifications;

public class AvailableRoomsSpecification : ISpecification<Room>
{
    private readonly DateTimeOffset _startTime;
    private readonly DateTimeOffset _endTime;
    private readonly int _capacity;

    public AvailableRoomsSpecification(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        int capacity)
    {
        _startTime = startTime;
        _endTime = endTime;
        _capacity = capacity;
    }

    public Expression<Func<Room, bool>> Criteria =>
        room =>
            room.Capacity >= _capacity &&
            !room.Bookings.Any(booking =>
                booking.StartTime < _endTime &&
                booking.EndTime > _startTime);
}