using Godot;
using System.Collections.Generic;

/*
 * Author: Klaykree https://github.com/klaykree
 * Translated from GDScript to C# by: Adrian Azan https://github.com/adrian-azan
 * https://github.com/klaykree/Godot-3D-Lines
 */

public partial class DrawLine3D : Node2D
{
    public static DrawLine3D Instance;

    private class Line
    {
        public Vector3 _Start;
        public Vector3 _End;
        public Color _Color;
        public float _Duration;

        public Line(Vector3 _Start, Vector3 _End, Color Color, float Time)
        {
            this._Start = _Start;
            this._End = _End;
            this._Color = Color;
            this._Duration = Time;
        }
    }

    private List<Line> _Lines = new List<Line>();
    private bool RemovedLine = false;

    public override void _Process(double delta)
    {
        foreach (Line line in _Lines)
            line._Duration -= (float)delta;

        if (_Lines.Count > 0 || RemovedLine)
        {
            QueueRedraw();
            RemovedLine = false;
        }
    }

    public override void _Draw()
    {
        Camera3D Cam = GetViewport().GetCamera3D();
        foreach (Line line in _Lines)
        {
            var ScreenPointStart = Cam.UnprojectPosition(line._Start);
            var ScreenPointEnd = Cam.UnprojectPosition(line._End);

            /*  Dont draw line if either start or end is considered behind the camera
		        this causes the line to not be drawn sometimes but avoids a bug where the
		        line is drawn incorrectly */
            if (Cam.IsPositionBehind(line._Start) || Cam.IsPositionBehind(line._End))
                continue;

            DrawLine(ScreenPointStart, ScreenPointEnd, line._Color);
        }

        //Remove lines that have timed out
        int i = _Lines.Count - 1;
        while (i >= 0)
        {
            if (_Lines[i]._Duration < 0)
            {
                _Lines.RemoveAt(i);
                RemovedLine = true;
            }
            i--;
        }
    }

    public void DrawLine(Vector3 Start, Vector3 End, Color LineColor, float Duration = 0.0f)
    {
        _Lines.Add(new Line(Start, End, LineColor, Duration));
    }

    public void DrawRay(Vector3 Start, Vector3 Ray, Color LineColor, float Duration = 0.0f)
    {
        _Lines.Add(new Line(Start, Start + Ray, LineColor, Duration));
    }

    public void DrawCube(Vector3 Center, float HalfExtents, Color LineColor, float _Duration = 0.0f)
    {
        //_Start at the 'top left'
        var LinePointStart = Center;
        LinePointStart.X -= HalfExtents;
        LinePointStart.Y += HalfExtents;
        LinePointStart.Z -= HalfExtents;

        //Draw top square
        var LinePointEnd = LinePointStart + new Vector3(0, 0, HalfExtents * 2.0f);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(HalfExtents * 2.0f, 0, 0);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(0, 0, -HalfExtents * 2.0f);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(-HalfExtents * 2.0f, 0, 0);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);

        //Draw bottom square
        LinePointStart = LinePointEnd + new Vector3(0, -HalfExtents * 2.0f, 0);
        LinePointEnd = LinePointStart + new Vector3(0, 0, HalfExtents * 2.0f);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(HalfExtents * 2.0f, 0, 0);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(0, 0, -HalfExtents * 2.0f);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);
        LinePointStart = LinePointEnd;
        LinePointEnd = LinePointStart + new Vector3(-HalfExtents * 2.0f, 0, 0);
        DrawLine(LinePointStart, LinePointEnd, LineColor, _Duration);

        //Draw vertical lines
        LinePointStart = LinePointEnd;
        DrawRay(LinePointStart, new Vector3(0, HalfExtents * 2.0f, 0), LineColor, _Duration);
        LinePointStart += new Vector3(0, 0, HalfExtents * 2.0f);
        DrawRay(LinePointStart, new Vector3(0, HalfExtents * 2.0f, 0), LineColor, _Duration);
        LinePointStart += new Vector3(HalfExtents * 2.0f, 0, 0);
        DrawRay(LinePointStart, new Vector3(0, HalfExtents * 2.0f, 0), LineColor, _Duration);
        LinePointStart += new Vector3(0, 0, -HalfExtents * 2.0f);
        DrawRay(LinePointStart, new Vector3(0, HalfExtents * 2.0f, 0), LineColor, _Duration);
    }
}