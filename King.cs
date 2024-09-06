using System;

public class King : cub
{
    public override void run(int x, int y)
    {
        if (base.CurrentY < y)
        {
            base.turnUp();
        }
        if (base.CurrentY > y)
        {
            base.turnDown();
        }
        if (base.CurrentX > x)
        {
            base.turnLeft();
        }
        if (base.CurrentX < x)
        {
            base.turnRight();
        }
        base.FinalRotation();
    }

    public override int getTurns()
    {
        return 1;
    }
}
