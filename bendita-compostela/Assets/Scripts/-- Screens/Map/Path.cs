using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private Vector2 origin;
    private Vector2 destination;

    public Path(Vector2 origin, Vector2 destination)
    {
        this.origin = origin;
        this.destination = destination;
    }

    public Vector2 Origin
    {
        get { return origin; }
    }
    public Vector2 Destination
    {
        get { return destination; }
    }

    public override bool Equals(object obj)
    {
        return obj is Path path &&
               origin.Equals(path.origin) &&
               destination.Equals(path.destination) &&
               Origin.Equals(path.Origin) &&
               Destination.Equals(path.Destination);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(origin, destination, Origin, Destination);
    }
    public override string ToString()
    {
        return $"Path:({origin}) - ({destination})";
    }
}