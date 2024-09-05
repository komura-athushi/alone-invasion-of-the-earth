public interface IObject
{
    enum EnAttribute
    {
        enAttribute_PlayerUpDown,
        enAttribute_None
    }

    // ダメージを与える
    public void Damage(int attack);
    // 無重力状態にさせる
    public void MakeZeroGravity();
    // 無重力状態を解除
    public void LoseZeroGravity();
    public EnAttribute IsAttribute() { return EnAttribute.enAttribute_None; }
}
