# Resource Gathering - Initial Setup
## **Player.cs**
Adding resource gathering variable:
+ _public int food_ ,
and a method to upate the resource after gathering
### public void GainResource(ResourceType resourceType,int amount)
Inside the function check the resource type we update 
```
switch(resourceType)
{
  case ResourceType.Food:
  {
    food += amount;
    break;
  }
}
```

## **ResourceSource.cs**
Call player's _GainResource()_ method
```
public void GatherResource(int amount,Player player)
{
  ...
  player.GainResource(resourceType, amount);
}
```
