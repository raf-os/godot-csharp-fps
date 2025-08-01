using Godot;

public interface IWeapon
{
    public bool Attack();
    public bool AttackAuto();
    public void OnMove();
    public void OnIdle();
    public void OnAir();
    public void OnReload();
    public void OnEquip();
    public void OnUnequip();
}