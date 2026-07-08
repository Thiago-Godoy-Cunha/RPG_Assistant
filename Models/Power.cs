using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public record ClassPower(
    ClassPowerType Id,          
    string Name,                
    ClassType AssociatedClass,  
    string Description,         
    string Requirements         
);
