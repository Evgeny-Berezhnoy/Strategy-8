﻿using UnityEngine;

[RequireComponent(typeof(OutlineDraw))]
public class MainBuilding : MonoBehaviour, ISelectable, IOutlinable, IPointable
{

    #region Fields

    [SerializeField] private Transform _unitParent;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;
    [SerializeField] private OutlineDraw _outlineDraw;

    private float _health = 1000;

    #endregion

    #region Interfaces Properties

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;

    #endregion

}