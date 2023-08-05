using Discoteque.Data.Models;

namespace Discoteque.Data.Dto;

public class TourmMessage : BaseMessage {
    public List<Tour> Tours { get; set; } = new();  
}