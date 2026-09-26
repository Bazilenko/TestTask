using Bogus;
using TestTask.DAL.Entities;

namespace TestTask.DAL.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        int roomCount,
        int maxBookingsPerRoom,
        CancellationToken cancellationToken)
    {
        if (roomCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(roomCount));
        }

        if (maxBookingsPerRoom < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxBookingsPerRoom));
        }

        // Don't seed an already populated database.
        if (context.Rooms.Any())
        {
            return;
        }

        var services = CreateServices();

        var rooms = CreateRooms(roomCount);

        AddRoomServices(rooms, services);

        AddBookings(
            rooms,
            maxBookingsPerRoom);

        await context.Services.AddRangeAsync(
            services,
            cancellationToken);

        await context.Rooms.AddRangeAsync(
            rooms,
            cancellationToken);

        await context.SaveChangesAsync(
            cancellationToken);
    }

    private static List<Service> CreateServices()
    {
        return new List<Service>
        {
            new Service
            {
                Name = "Projector",
                Price = 500
            },
            new Service
            {
                Name = "Wi-Fi",
                Price = 300
            },
            new Service
            {
                Name = "Sound",
                Price = 700
            }
        };
    }

    private static List<Room> CreateRooms(int count)
    {
        var faker = new Faker<Room>()
            .RuleFor(
                room => room.Name,
                (faker, room) =>
                    $"Зал {faker.UniqueIndex + 1}")
            .RuleFor(
                room => room.Capacity,
                faker => faker.Random.Int(10, 200))
            .RuleFor(
                room => room.HourlyRate,
                faker => faker.Random.Decimal(
                    1000,
                    10000));

        return faker.Generate(count);
    }

    private static void AddRoomServices(
        List<Room> rooms,
        List<Service> services)
    {
        var random = new Random();

        foreach (var room in rooms)
        {
            var serviceCount = random.Next(
                1,
                services.Count + 1);

            var selectedServices = services
                .OrderBy(_ => random.Next())
                .Take(serviceCount);

            foreach (var service in selectedServices)
            {
                room.RoomServices.Add(
                    new RoomService
                    {
                        Room = room,
                        Service = service
                    });
            }
        }
    }

    private static void AddBookings(
        List<Room> rooms,
        int maxBookingsPerRoom)
    {
        var faker = new Faker();

        foreach (var room in rooms)
        {
            var bookingCount = faker.Random.Int(
                0,
                maxBookingsPerRoom);

            var occupiedSlots = new List<(DateTimeOffset Start, DateTimeOffset End)>();

            for (var i = 0; i < bookingCount; i++)
            {
                var slot = GenerateAvailableSlot(
                    occupiedSlots,
                    faker);

                if (slot is null)
                {
                    break;
                }

                occupiedSlots.Add(slot.Value);

                var booking = new Booking
                {
                    Room = room,
                    StartTime = slot.Value.Start,
                    EndTime = slot.Value.End
                };

                AddBookingServices(
                    booking,
                    room,
                    faker);

                booking.TotalPrice = CalculateTotalPrice(
                    booking);

                room.Bookings.Add(booking);
            }
        }
    }

    private static (DateTimeOffset Start, DateTimeOffset End)?
        GenerateAvailableSlot(
            List<(DateTimeOffset Start, DateTimeOffset End)> occupiedSlots,
            Faker faker)
    {
        for (var attempt = 0; attempt < 20; attempt++)
        {
            var futureDate = faker.Date.Between(
                    DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(365));

            var startHour = faker.Random.Int(6, 20);

            var startTime = new DateTimeOffset(
                futureDate.Year,
                futureDate.Month,
                futureDate.Day,
                startHour,
                0,
                0,
                TimeSpan.Zero);

            var duration = faker.Random.Int(1, 3);

            var endTime = startTime.AddHours(duration);

            // Don't allow bookings after 23:00.
            if (endTime.Hour > 23 ||
                (endTime.Hour == 23 && endTime.Minute > 0))
            {
                continue;
            }

            var overlaps = occupiedSlots.Any(slot =>
                startTime < slot.End &&
                endTime > slot.Start);

            if (!overlaps)
            {
                return (startTime, endTime);
            }
        }

        return null;
    }

    private static void AddBookingServices(
        Booking booking,
        Room room,
        Faker faker)
    {
        var availableServices = room.RoomServices
            .Select(roomService => roomService.Service)
            .ToList();

        if (availableServices.Count == 0)
        {
            return;
        }

        var serviceCount = faker.Random.Int(
            0,
            availableServices.Count);

        var selectedServices = availableServices
            .OrderBy(_ => faker.Random.Int())
            .Take(serviceCount);

        foreach (var service in selectedServices)
        {
            booking.BookingServices.Add(
                new BookingService
                {
                    Booking = booking,
                    Service = service,

                    // Save the service price at the moment
                    // of the booking.
                    Price = service.Price
                });
        }
    }

    private static decimal CalculateTotalPrice(
        Booking booking)
    {
        var hours = (decimal)(
            booking.EndTime - booking.StartTime)
            .TotalHours;

        var roomPrice =
            hours * booking.Room.HourlyRate;

        var servicesPrice = booking.BookingServices
            .Sum(bookingService => bookingService.Price);

        return roomPrice + servicesPrice;
    }
}