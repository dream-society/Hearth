using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public interface ICollectable
{
    public void Collect(CharacterController2D player);
}
