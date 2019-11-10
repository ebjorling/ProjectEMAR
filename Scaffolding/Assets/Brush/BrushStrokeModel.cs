using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class BrushStrokeModel {
    [RealtimeProperty(1, true)]
    private RealtimeArray<RibbonPointModel> _ribbonPoints;

    [RealtimeProperty(2, false)]
    private Vector3 _brushTipPosition;

    [RealtimeProperty(3, false)]
    private Quaternion _brushTipRotation;

    [RealtimeProperty(4, true)]
    private bool _brushStrokeFinalized;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class BrushStrokeModel : IModel {
    // Properties
    public Normal.Realtime.Serialization.RealtimeArray<RibbonPointModel> ribbonPoints {
        get { return _ribbonPoints; }
    }
    public UnityEngine.Vector3 brushTipPosition {
        get { return _brushTipPosition; }
        set { if (value == _brushTipPosition) return; _brushTipPositionShouldWrite = true; _brushTipPosition = value; }
    }
    public UnityEngine.Quaternion brushTipRotation {
        get { return _brushTipRotation; }
        set { if (value == _brushTipRotation) return; _brushTipRotationShouldWrite = true; _brushTipRotation = value; }
    }
    public bool brushStrokeFinalized {
        get { return _cache.LookForValueInCache(_brushStrokeFinalized, entry => entry.brushStrokeFinalizedSet, entry => entry.brushStrokeFinalized); }
        set { if (value == brushStrokeFinalized) return; _cache.UpdateLocalCache(entry => { entry.brushStrokeFinalizedSet = true; entry.brushStrokeFinalized = value; return entry; }); }
    }
    
    // Delta updates
    private struct LocalCacheEntry {
        public bool brushStrokeFinalizedSet;
        public bool brushStrokeFinalized;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache;
    
    private bool _brushTipPositionShouldWrite;
    private bool _brushTipRotationShouldWrite;
    
    public BrushStrokeModel() {
        _cache = new LocalChangeCache<LocalCacheEntry>();
        
        _ribbonPoints = new Normal.Realtime.Serialization.RealtimeArray<RibbonPointModel>();
    }
    
    // Serialization
    enum PropertyID {
        RibbonPoints = 1,
        BrushTipPosition = 2,
        BrushTipRotation = 3,
        BrushStrokeFinalized = 4,
    }
    
    public int WriteLength(StreamContext context) {
        int length = 0;
        
        if (context.fullModel) {
            // Mark unreliable properties as clean and flatten the in-flight cache.
            // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
            _brushTipPositionShouldWrite = false;
            _brushTipRotationShouldWrite = false;
            _brushStrokeFinalized = brushStrokeFinalized;
            _cache.Clear();
            
            // Write all properties
            length += WriteStream.WriteCollectionLength((uint)PropertyID.RibbonPoints, _ribbonPoints, context);
            length += WriteStream.WriteBytesLength((uint)PropertyID.BrushTipPosition, WriteStream.Vector3ToBytesLength());
            length += WriteStream.WriteBytesLength((uint)PropertyID.BrushTipRotation, WriteStream.QuaternionToBytesLength());
            length += WriteStream.WriteVarint32Length((uint)PropertyID.BrushStrokeFinalized, _brushStrokeFinalized ? 1u : 0u);
        } else {
            // Unreliable properties
            if (context.unreliableChannel) {
                if (_brushTipPositionShouldWrite) {
                    length += WriteStream.WriteBytesLength((uint)PropertyID.BrushTipPosition, WriteStream.Vector3ToBytesLength());
                }
                if (_brushTipRotationShouldWrite) {
                    length += WriteStream.WriteBytesLength((uint)PropertyID.BrushTipRotation, WriteStream.QuaternionToBytesLength());
                }
            }
            
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.brushStrokeFinalizedSet)
                    length += WriteStream.WriteVarint32Length((uint)PropertyID.BrushStrokeFinalized, entry.brushStrokeFinalized ? 1u : 0u);
            }
            
            // Models
            length += WriteStream.WriteCollectionLength((uint)PropertyID.RibbonPoints, _ribbonPoints, context);
        }
        
        return length;
    }
    
    public void Write(WriteStream stream, StreamContext context) {
        if (context.fullModel) {
            // Write all properties
            stream.WriteCollection((uint)PropertyID.RibbonPoints, _ribbonPoints, context);
            stream.WriteBytes((uint)PropertyID.BrushTipPosition, WriteStream.Vector3ToBytes(_brushTipPosition));
            _brushTipPositionShouldWrite = false;
            stream.WriteBytes((uint)PropertyID.BrushTipRotation, WriteStream.QuaternionToBytes(_brushTipRotation));
            _brushTipRotationShouldWrite = false;
            stream.WriteVarint32((uint)PropertyID.BrushStrokeFinalized, _brushStrokeFinalized ? 1u : 0u);
        } else {
            // Unreliable properties
            if (context.unreliableChannel) {
                if (_brushTipPositionShouldWrite) {
                    stream.WriteBytes((uint)PropertyID.BrushTipPosition, WriteStream.Vector3ToBytes(_brushTipPosition));
                    _brushTipPositionShouldWrite = false;
                }
                if (_brushTipRotationShouldWrite) {
                    stream.WriteBytes((uint)PropertyID.BrushTipRotation, WriteStream.QuaternionToBytes(_brushTipRotation));
                    _brushTipRotationShouldWrite = false;
                }
            }
            
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.brushStrokeFinalizedSet)
                    _cache.PushLocalCacheToInflight(context.updateID);
                
                if (entry.brushStrokeFinalizedSet)
                    stream.WriteVarint32((uint)PropertyID.BrushStrokeFinalized, entry.brushStrokeFinalized ? 1u : 0u);
            }
            
            // Models
            stream.WriteCollection((uint)PropertyID.RibbonPoints, _ribbonPoints, context);
        }
    }
    
    public void Read(ReadStream stream, StreamContext context) {
        // Remove from in-flight
        if (context.deltaUpdatesOnly && context.reliableChannel)
            _cache.RemoveUpdateFromInflight(context.updateID);
        
        // Loop through each property and deserialize
        uint propertyID;
        while (stream.ReadNextPropertyID(out propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.RibbonPoints: {
                    stream.ReadCollection(_ribbonPoints, context);
                    break;
                }
                case (uint)PropertyID.BrushTipPosition: {
                    _brushTipPosition = ReadStream.Vector3FromBytes(stream.ReadBytes());
                    _brushTipPositionShouldWrite = false;
                    break;
                }
                case (uint)PropertyID.BrushTipRotation: {
                    _brushTipRotation = ReadStream.QuaternionFromBytes(stream.ReadBytes());
                    _brushTipRotationShouldWrite = false;
                    break;
                }
                case (uint)PropertyID.BrushStrokeFinalized: {
                    _brushStrokeFinalized = (stream.ReadVarint32() != 0);
                    break;
                }
                default:
                    stream.SkipProperty();
                    break;
            }
        }
    }
}
/* ----- End Normal Autogenerated Code ----- */
