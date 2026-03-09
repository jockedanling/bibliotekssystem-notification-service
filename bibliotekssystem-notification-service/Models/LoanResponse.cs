namespace bibliotekssystem_notification_service.Models;

// HTTP-anrop till LoanService kommer ge tillbaka JSON och .NET behöver kunna översätta JSON till C#
// Och det är vad denna klass är till för.
public class LoanResponse
{
    public int Id { get; set; }
    public int BorrowerId { get; set; }
    public int ItemId { get; set; }
    public decimal Amount { get; set; }
    public DateTime LoanDate  { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
}