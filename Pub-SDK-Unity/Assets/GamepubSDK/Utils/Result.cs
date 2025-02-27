﻿using System;

namespace GamePub.PubSDK
{
    public class Result<T>
    {

        private T value;
        private PubSdkError error;

        private bool success;
       
        public bool IsSuccess { get { return success; } }
        
        public bool IsFailure { get { return !success; } }
        
        public static Result<T> Ok(T value)
        {
            var r = new Result<T>();
            r.value = value;
            r.success = true;
            return r;
        }
        
        public static Result<T> Error(PubSdkError error)
        {
            var r = new Result<T>();
            r.error = error;
            r.success = false;
            return r;
        }
        
        public void MatchOk(System.Action<T> onMatched)
        {
            if (onMatched == null) throw new System.Exception("Match callback is null!");
            if (IsSuccess) { onMatched(value); }
        }
       
        public void MatchError(System.Action<PubSdkError> onMatched)
        {
            if (onMatched == null) throw new System.Exception("Match callback is null!");
            if (IsFailure) { onMatched(error); }
        }
        
        public void Match(System.Action<T> onMatchedOk, System.Action<PubSdkError> onMatchedError)
        {
            MatchOk(onMatchedOk);
            MatchError(onMatchedError);
        }
    }

    public class Unit
    {
        public static Unit Value = new Unit();        
    }
}