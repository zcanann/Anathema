// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

//------------------------------------------------------------------------------
// <auto-generated>
//     Types declaration for SharpDX.XAudio2.Fx namespace.
//     This code was generated by a tool.
//     Date : 6/25/2016 10:38:15 PM
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Security;
namespace SharpDX.XAudio2.Fx {

#pragma warning disable 419
#pragma warning disable 1587
#pragma warning disable 1574

        /// <summary>	
        /// Functions	
        /// </summary>	
        /// <include file='..\..\Documentation\CodeComments.xml' path="/comments/comment[@id='SharpDX.XAudio2.Fx.Reverb']/*"/>	
    public  partial class Reverb {   
        
        /// <summary>Constant MinWetDryMix.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_WET_DRY_MIX</unmanaged>
        public const float MinWetDryMix = 0.0f;
        
        /// <summary>Constant MinReflectionsDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_REFLECTIONS_DELAY</unmanaged>
        public const int MinReflectionsDelay = 0;
        
        /// <summary>Constant MinReverbDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_REVERB_DELAY</unmanaged>
        public const byte MinReverbDelay = 0;
        
        /// <summary>Constant MinRearDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_REAR_DELAY</unmanaged>
        public const byte MinRearDelay = 0;
        
        /// <summary>Constant MinPosition.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_POSITION</unmanaged>
        public const byte MinPosition = 0;
        
        /// <summary>Constant MinDiffusion.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_DIFFUSION</unmanaged>
        public const byte MinDiffusion = 0;
        
        /// <summary>Constant MinLowEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_LOW_EQ_GAIN</unmanaged>
        public const byte MinLowEqGain = 0;
        
        /// <summary>Constant MinLowEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_LOW_EQ_CUTOFF</unmanaged>
        public const byte MinLowEqCutoff = 0;
        
        /// <summary>Constant MinHighEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_HIGH_EQ_GAIN</unmanaged>
        public const byte MinHighEqGain = 0;
        
        /// <summary>Constant MinHighEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_HIGH_EQ_CUTOFF</unmanaged>
        public const byte MinHighEqCutoff = 0;
        
        /// <summary>Constant MinRoomFilterFreq.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_ROOM_FILTER_FREQ</unmanaged>
        public const float MinRoomFilterFreq = 2.0e+1f;
        
        /// <summary>Constant MinRoomFilterMain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_ROOM_FILTER_MAIN</unmanaged>
        public const float MinRoomFilterMain = -1.0e+2f;
        
        /// <summary>Constant MinRoomFilterHf.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_ROOM_FILTER_HF</unmanaged>
        public const float MinRoomFilterHf = -1.0e+2f;
        
        /// <summary>Constant MinReflectionsGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_REFLECTIONS_GAIN</unmanaged>
        public const float MinReflectionsGain = -1.0e+2f;
        
        /// <summary>Constant MinReverbGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_REVERB_GAIN</unmanaged>
        public const float MinReverbGain = -1.0e+2f;
        
        /// <summary>Constant MinDecayTime.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_DECAY_TIME</unmanaged>
        public const float MinDecayTime = 1.00000001490116119384765625e-1f;
        
        /// <summary>Constant MinDensity.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_DENSITY</unmanaged>
        public const float MinDensity = 0.0f;
        
        /// <summary>Constant MinRoomSize.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MIN_ROOM_SIZE</unmanaged>
        public const float MinRoomSize = 0.0f;
        
        /// <summary>Constant MaxWetDryMix.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_WET_DRY_MIX</unmanaged>
        public const float MaxWetDryMix = 1.0e+2f;
        
        /// <summary>Constant MaxReflectionsDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_REFLECTIONS_DELAY</unmanaged>
        public const int MaxReflectionsDelay = 300;
        
        /// <summary>Constant MaxReverbDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_REVERB_DELAY</unmanaged>
        public const byte MaxReverbDelay = 85;
        
        /// <summary>Constant MaxRearDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_REAR_DELAY</unmanaged>
        public const byte MaxRearDelay = 5;
        
        /// <summary>Constant MaxPosition.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_POSITION</unmanaged>
        public const byte MaxPosition = 30;
        
        /// <summary>Constant MaxDiffusion.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_DIFFUSION</unmanaged>
        public const byte MaxDiffusion = 15;
        
        /// <summary>Constant MaxLowEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_LOW_EQ_GAIN</unmanaged>
        public const byte MaxLowEqGain = 12;
        
        /// <summary>Constant MaxLowEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_LOW_EQ_CUTOFF</unmanaged>
        public const byte MaxLowEqCutoff = 9;
        
        /// <summary>Constant MaxHighEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_HIGH_EQ_GAIN</unmanaged>
        public const byte MaxHighEqGain = 8;
        
        /// <summary>Constant MaxHighEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_HIGH_EQ_CUTOFF</unmanaged>
        public const byte MaxHighEqCutoff = 14;
        
        /// <summary>Constant MaxRoomFilterFreq.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_ROOM_FILTER_FREQ</unmanaged>
        public const float MaxRoomFilterFreq = 2.0e+4f;
        
        /// <summary>Constant MaxRoomFilterMain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_ROOM_FILTER_MAIN</unmanaged>
        public const float MaxRoomFilterMain = 0.0f;
        
        /// <summary>Constant MaxRoomFilterHf.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_ROOM_FILTER_HF</unmanaged>
        public const float MaxRoomFilterHf = 0.0f;
        
        /// <summary>Constant MaxReflectionsGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_REFLECTIONS_GAIN</unmanaged>
        public const float MaxReflectionsGain = 2.0e+1f;
        
        /// <summary>Constant MaxReverbGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_REVERB_GAIN</unmanaged>
        public const float MaxReverbGain = 2.0e+1f;
        
        /// <summary>Constant MaxDensity.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_DENSITY</unmanaged>
        public const float MaxDensity = 1.0e+2f;
        
        /// <summary>Constant MaxRoomSize.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_MAX_ROOM_SIZE</unmanaged>
        public const float MaxRoomSize = 1.0e+2f;
        
        /// <summary>Constant DefaultWetDryMix.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_WET_DRY_MIX</unmanaged>
        public const float DefaultWetDryMix = 1.0e+2f;
        
        /// <summary>Constant DefaultReflectionsDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_REFLECTIONS_DELAY</unmanaged>
        public const int DefaultReflectionsDelay = 5;
        
        /// <summary>Constant DefaultReverbDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_REVERB_DELAY</unmanaged>
        public const byte DefaultReverbDelay = 5;
        
        /// <summary>Constant DefaultRearDelay.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_REAR_DELAY</unmanaged>
        public const byte DefaultRearDelay = 5;
        
        /// <summary>Constant DefaultPosition.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_POSITION</unmanaged>
        public const byte DefaultPosition = 6;
        
        /// <summary>Constant DefaultPositionMatrix.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_POSITION_MATRIX</unmanaged>
        public const byte DefaultPositionMatrix = 27;
        
        /// <summary>Constant DefaultEarlyDiffusion.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_EARLY_DIFFUSION</unmanaged>
        public const byte DefaultEarlyDiffusion = 8;
        
        /// <summary>Constant DefaultLateDiffusion.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_LATE_DIFFUSION</unmanaged>
        public const byte DefaultLateDiffusion = 8;
        
        /// <summary>Constant DefaultLowEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_LOW_EQ_GAIN</unmanaged>
        public const byte DefaultLowEqGain = 8;
        
        /// <summary>Constant DefaultLowEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_LOW_EQ_CUTOFF</unmanaged>
        public const byte DefaultLowEqCutoff = 4;
        
        /// <summary>Constant DefaultHighEqGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_HIGH_EQ_GAIN</unmanaged>
        public const byte DefaultHighEqGain = 8;
        
        /// <summary>Constant DefaultHighEqCutoff.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_HIGH_EQ_CUTOFF</unmanaged>
        public const byte DefaultHighEqCutoff = 4;
        
        /// <summary>Constant DefaultRoomFilterFreq.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_FREQ</unmanaged>
        public const float DefaultRoomFilterFreq = 5.0e+3f;
        
        /// <summary>Constant DefaultRoomFilterMain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_MAIN</unmanaged>
        public const float DefaultRoomFilterMain = 0.0f;
        
        /// <summary>Constant DefaultRoomFilterHf.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_HF</unmanaged>
        public const float DefaultRoomFilterHf = 0.0f;
        
        /// <summary>Constant DefaultReflectionsGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_REFLECTIONS_GAIN</unmanaged>
        public const float DefaultReflectionsGain = 0.0f;
        
        /// <summary>Constant DefaultReverbGain.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_REVERB_GAIN</unmanaged>
        public const float DefaultReverbGain = 0.0f;
        
        /// <summary>Constant DefaultDecayTime.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_DECAY_TIME</unmanaged>
        public const float DefaultDecayTime = 1.0e+0f;
        
        /// <summary>Constant DefaultDensity.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_DENSITY</unmanaged>
        public const float DefaultDensity = 1.0e+2f;
        
        /// <summary>Constant DefaultRoomSize.</summary>
        /// <unmanaged>XAUDIO2FX_REVERB_DEFAULT_ROOM_SIZE</unmanaged>
        public const float DefaultRoomSize = 1.0e+2f;
    }
}
