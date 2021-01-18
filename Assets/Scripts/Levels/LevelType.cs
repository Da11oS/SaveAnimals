using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

abstract public class LevelType: ILevel
{
    public Level Parent;
    public Levels Levels { get => Levels.Instance; }
    public LevelType(Level parent)
    {
        Parent = parent;
    }
    public LevelType()
    {

    }
    public abstract void Open();
    public abstract void Close();
    public abstract void Finish();
};
public class OpenLevel : LevelType
{
    public OpenLevel(Level parent):base(parent)
    {

    }
    public OpenLevel() : base()
    {

    }
    override public void Open()
    {
        Parent.gameObject.SetActive(true);
    }
    override public void Close()
    {
        Parent.gameObject.SetActive(false);
    }
    override public void Finish()
    {
        Levels.SetLevelIsOpen(Parent.Id + 1);
    }
};

public class CloseLevel: LevelType
{
    public CloseLevel(Level parent) : base(parent)
    {

    }
    override public void Open()
    {

    }
    override public void Close()
    {

    }
    override public void Finish()
    {

    }
};
interface ILevel
{
    void Open();
    void Close();
    void Finish();
};
public enum Status { Open, Close, Current };
