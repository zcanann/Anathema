﻿using System;

namespace Anathena.Source.Engine.Graphics
{
    public interface IGraphicsInterface
    {
        Guid CreateText(String Text, Int32 LocationX, Int32 LocationY);

        Guid CreateImage(String FileName, Int32 LocationX, Int32 LocationY);

        void DestroyObject(Guid Guid);

        void ShowObject(Guid Guid);

        void HideObject(Guid Guid);

    } // End interface

} // End namespace