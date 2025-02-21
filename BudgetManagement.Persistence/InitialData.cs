using BudgetManagement.Core.Entities;

namespace BudgetManagement.Persistence;

internal static class InitialData
{
    #region Users

    public static readonly User[] Users = [
        new()
        {
            Id = Guid.Parse("0c2ed985-9fa1-415d-8a19-005bd929fd71"),
            Login = "test@gmail.com",
            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" // test
        },
        new()
        {
            Id = Guid.Parse("20d89285-d9e5-495b-89d5-469d751d111e"),
            Login = "testtwo@gmail.com",
            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
        },
        new()
        {
            Id = Guid.Parse("515f0613-b25e-41f0-980b-41eef57eae8a"),
            Login = "testthree@gmail.com",
            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
        },
        new()
        {
            Id = Guid.Parse("f2c61707-cb8e-44d0-8ed8-3cec4b2d237a"),
            Login = "testfour@gmail.com",
            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
        }
    ];

    #endregion

    #region ExpenseCategories

    public static readonly ExpenseCategory[] ExpenseCategories = [
        new()
        {
            Id = Guid.Parse("974fe237-5533-4898-aa01-912455c656d4"),
            Name = "Food",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("8b506abc-fa2e-46dd-9cd6-b888e3330a5a"),
            Name = "Transport",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("80883d5e-da68-40ac-b7dc-e0c50a63acb7"),
            Name = "Entertainment",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("67b3701d-002c-4a72-8a31-09c6bc362c1c"),
            Name = "Health",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("fa430763-9dba-444e-b840-46fb5e4f1088"),
            Name = "Education",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("cf137382-a857-4ce9-91ad-7658346ed408"),
            Name = "Shopping",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("f19f6a74-4c97-41bf-9bc7-880dc4f212a5"),
            Name = "Bills",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("b9faad8d-ee0b-46c7-ac51-505f867c0fd7"),
            Name = "Savings",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("9fa2b238-45b8-4243-b29a-0bf6cd25479c"),
            Name = "Gifts",
            UserId = null,
            RootId = null
        },
        new()
        {
            Id = Guid.Parse("922e4067-56b5-4a20-987f-43285fd66a43"),
            Name = "Travel",
            UserId = null,
            RootId = null
        }
    ];

    #endregion

    #region Transactions

    public static readonly ExpenseRecord[] ExpenseRecords = [
        new()
        {
            Id = Guid.Parse("18d60b25-98c1-4a36-afe6-f0b706b13bff"),
            Amount = 50.75m,
            CreatedAt = DateTime.UtcNow,
            CategoryId = ExpenseCategories[0].Id,
            UserId = Users[0].Id
        },
        new()
        {
            Id = Guid.Parse("1888051e-3b87-4bbf-bce7-0d9ea38dce3c"),
            Amount = 15.30m,
            CreatedAt = DateTime.UtcNow,
            CategoryId = ExpenseCategories[1].Id,
            UserId = Users[1].Id
        },
        new()
        {
            Id = Guid.Parse("9d8c1f84-6dbf-4476-9039-aec43c5ff873"),
            Amount = 100.00m,
            CreatedAt = DateTime.UtcNow,
            CategoryId = ExpenseCategories[2].Id,
            UserId = Users[2].Id
        },
        new()
        {
            Id = Guid.Parse("f4fb757d-92a8-417c-9d34-4303ac3211dc"),
            Amount = 200.50m,
            CreatedAt = DateTime.UtcNow,
            CategoryId = ExpenseCategories[3].Id,
            UserId = Users[3].Id
        }
    ];

    #endregion
}
