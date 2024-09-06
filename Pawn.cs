using System;

public class Pawn : cub
{
    public override void run(int x, int y)
    {
        if (this.Turns > 0)
        {
            if (!this.lineX)
            {
                if (base.CurrentY <= y || base.CurrentY >= y)
                {
                    if (base.CurrentY < y && this.up[x, y])
                    {
                        base.turnUp();
                        base.FinalRotation();
                    }
                    else if (base.CurrentY > y && this.down[x, y])
                    {
                        base.turnDown();
                        base.FinalRotation();
                    }
                    else if (base.CurrentY == y)
                    {
                        if (base.CurrentX > x)
                        {
                            base.turnLeft();
                            base.FinalRotation();
                        }
                        else if (base.CurrentX < x)
                        {
                            base.turnRight();
                            base.FinalRotation();
                        }
                    }
                }
            }
            else if (base.CurrentX <= x || base.CurrentX >= x)
            {
                if (base.CurrentX < x && this.right[x, y])
                {
                    base.turnRight();
                    base.FinalRotation();
                }
                else if (base.CurrentX > x && this.left[x, y])
                {
                    base.turnLeft();
                    base.FinalRotation();
                }
                else if (base.CurrentX == x)
                {
                    if (base.CurrentY < y)
                    {
                        base.turnUp();
                        base.FinalRotation();
                    }
                    else if (base.CurrentY > y)
                    {
                        base.turnDown();
                        base.FinalRotation();
                    }
                }
            }
        }
    }

    public override int getTurns()
    {
        int result;
        if ((this.rX == 0f && this.rZ == 0f) || (this.rX == 180f && this.rZ == 180f))
        {
            result = 1;
        }
        else if (this.rX == 90f)
        {
            result = 2;
        }
        else if ((this.rX == 180f && this.rZ == 270f) || (this.rX == 0f && this.rZ == 90f))
        {
            result = 3;
        }
        else if ((this.rX == 180f && this.rZ == 90f) || (this.rX == 0f && this.rZ == 270f))
        {
            result = 4;
        }
        else if (this.rX == 270f)
        {
            result = 5;
        }
        else if ((this.rX == 180f && this.rZ == 0f) || (this.rX == 0f && this.rZ == 180f))
        {
            result = 6;
        }
        else
        {
            result = 0;
        }
        return result;
    }
}
