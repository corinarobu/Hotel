using Hotel.HotelManagement.Entities;

public class FAQ
{
    public int Id_FAQ { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int? IdRoom { get; set; }
    public Room Room { get; set; } = new Room();


}
