using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour {
    [SerializeField]
    private int _frameRange = 60;

    private int[] _fpsBuffer;
    private int _fpsBufferIndex;

    public int AverageFPS { get; private set; }

    private void Update() {
        if (_fpsBuffer == null) {
            InitializeBuffer();
        }

        UpdateBuffer();
        CalculateFps();
    }

    private void InitializeBuffer() {
        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer() {
        _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (_fpsBufferIndex >= _frameRange) {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFps() {
        int sum = 0;
        
        for (int i = 0; i < _frameRange; i++) {
            int fps = _fpsBuffer[i];
            sum += fps;
        }
        AverageFPS = sum / _frameRange;
    }
}

