using System;

// <summary>
//Todo: צרו מחלקת חריגים המטפלת במקרים בהם אין מספיק מחשבים זמינים להשאלה.
//שם המחלקה צריך להיות EquipmentUnavailableException.
class EquipmentUnavailableException : Exception
{
    public int Demand { get; private set; }
    public int MissingAmount { get; private set; }
    public EquipmentUnavailableException(string message, int demand, int missingAmount) : base(message)
    {
        Demand = demand;
        MissingAmount = missingAmount;
    }
}



class EquipmentRoom
{
    private int availableComputers;

    public int AvailableComputers
    {
        get
        {
            return availableComputers;
        }
        set
        {
            // TODO: יש לבדוק שמספר המחשבים אינו שלילי.
            // אם כן, יש לזרוק חריגה מתאימה.
            if (value < 0)
                throw new ArgumentException("Number must not be negative");
                
            availableComputers = value;
        }
    }

    public EquipmentRoom(int amount)
    {
        AvailableComputers = amount;
    }

    public void Borrow(int amount)
    {
       
            // TODO: יש לזרוק חריגה מתאימה כאשר אין מספיק מחשבים זמינים.
            if (amount > AvailableComputers)
            {
                int missing = amount - AvailableComputers;
                throw new EquipmentUnavailableException("There are not enough available computers", amount, missing);
            }
        AvailableComputers -= amount;
        Console.WriteLine($"Borrowed successfully {amount} computers.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        int initialComputers = 10;

        EquipmentRoom room = new EquipmentRoom(initialComputers);

        // יצירת חדר מחשבים עם מספר מחשבים שלילי:

        // לאחר מימוש הבדיקה ב-set, יש לוודא שמצב כזה מטופל כראוי.
        // TODO: יש להוסיף try-catch מתאימים סביב הקריאות שעלולות לזרוק חריגה.
        
        try
        {
            int negativeComputers = -5;
            EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        try
        {
            room.Borrow(4);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Demand: {ex.Demand}, Missing Amount: {ex.MissingAmount}");
        }
        Console.WriteLine("continue on...");
        try
        {
            room.Borrow(8);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Demand: {ex.Demand}, Missing Amount: {ex.MissingAmount}");
        }

        Console.WriteLine("available computers " + room.AvailableComputers);
    }
}
