
////////////////////////////////////////////////////////////////////////////////////////////////
// ISP Violation
////////////////////////////////////////////////////////////////////////////////////////////////

public interface IMediaPlayer
{
void PlayAudio();
void PlayVideo();
void DisplaySubtitles();
void LoadMedia(string filePath);
}
public class AudioPlayer : IMediaPlayer
{
public void PlayAudio()
{
// Code to play audio
}
public void PlayVideo()
{
throw new NotImplementedException("Audio players cannot play videos.");
}
public void DisplaySubtitles()
{
throw new NotImplementedException("Audio players cannot display subtitles.");
}
public void LoadMedia(string filePath)
{
// Code to load audio file
}
}
public class VideoPlayer : IMediaPlayer
{
public void PlayAudio()
{
throw new NotImplementedException("Video players cannot play audio without video.");
}
public void PlayVideo()
{
// Code to play video
}
public void DisplaySubtitles()
{
// Code to display subtitles
}
public void LoadMedia(string filePath)
{
// Code to load video file
}
}



////////////////////////////////////////////////////////////////////////////////////////////////
// Identification and Resolving
////////////////////////////////////////////////////////////////////////////////////////////////




////////////////////////////////////////////////////////////////////////////////////////////////
//Identification:
////////////////////////////////////////////////////////////////////////////////////////////////

// In the original code, the IMediaPlayer interface violates the Interface Segregation Principle (ISP)
// by defining methods that are not relevant to all implementing classes. Specifically, the PlayVideo()
// and DisplaySubtitles() methods are not applicable to the AudioPlayer class, and the PlayAudio() method
// is not applicable to the VideoPlayer class. This results in unnecessary dependencies and potential
// NotImplementedExceptions being thrown in the implementing classes.

// The Identification section identifies the violation of the ISP in the original code, highlighting
// the need to refactor the interface to segregate its members into smaller, more cohesive interfaces
// that are specific to the needs of each implementing class.





////////////////////////////////////////////////////////////////////////////////////////////////
//  Resolving:
////////////////////////////////////////////////////////////////////////////////////////////////


// To resolve the ISP violation, the interfaces have been refactored to segregate their members into
// smaller, more specialized interfaces that are specific to the functionality required by each implementing
// class. This approach adheres to the Interface Segregation Principle by ensuring that clients are not
// forced to depend on interfaces they do not use.

// The Resolving section introduces several new interfaces: IAudioMediaPlayer, IVideoMediaPlayer, IAudioPlayer,
// and IVideoPlayer. These interfaces define subsets of the functionality originally present in IMediaPlayer,
// focusing on audio-related and video-related operations.

// Additionally, the AudioPlayer and VideoPlayer classes now implement the appropriate interfaces based on
// their functionality. AudioPlayer implements IAudioPlayer, which includes methods for playing audio and
// loading media files, while VideoPlayer implements IVideoPlayer, which includes methods for playing video,
// displaying subtitles, and loading media files.

// With this refactoring, each class and interface is now more focused and adheres to the Single Responsibility
// Principle (SRP) and the Interface Segregation Principle (ISP).




public interface IMediaPlayer
{
    void LoadMedia(string filePath);
}

public interface IAudioMediaPlayer
{
    void PlayAudio();
}

public interface  IVideoMediaPlayer
{
    void DisplaySubtitles();
    void PlayVideo();
}

public interface IAudioPlayer : IMediaPlayer, IAudioMediaPlayer
{

}

public interface IVideoPlayer : IMediaPlayer, IVideoMediaPlayer
{

}

public class AudioPlayer : IAudioPlayer
{
public void PlayAudio()
{
// Code to play audio
}
public void LoadMedia(string filePath)
{
// Code to load audio file
}
}

public class VideoPlayer : IVideoPlayer
{
public void PlayVideo()
{
// Code to play video
}
public void DisplaySubtitles()
{
// Code to display subtitles
}
public void LoadMedia(string filePath)
{
// Code to load video file
}
}