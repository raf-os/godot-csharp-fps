using System;

public interface IWeapon
{
    public bool Attack();
    public void OnMove();
    public void OnIdle();
    public void OnReload();
}