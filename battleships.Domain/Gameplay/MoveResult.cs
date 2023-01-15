using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay;

public record MoveResult((ShootResult ShootResult, string HitShipName, ShipStatus HitShipStatus) Result, bool GameOver)
{
    public override string ToString()
    {
        if (Result.ShootResult == ShootResult.Hit)
        {
            return $"Result: {ShootResult.Hit}. Ship: {Result.HitShipName}. Ship status: {Result.HitShipStatus}";
        }
        else
        {
            return "Miss";
        }
    }
}
