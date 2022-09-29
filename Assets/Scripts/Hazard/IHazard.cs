using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public interface IHazard
{
    public void Damage(CharacterController2D player);
}
