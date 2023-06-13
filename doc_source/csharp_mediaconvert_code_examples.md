# MediaConvert examples using AWS SDK for \.NET<a name="csharp_mediaconvert_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with MediaConvert\.

*Actions* are code excerpts from larger programs and must be run in context\. While actions show you how to call individual service functions, you can see actions in context in their related scenarios and cross\-service examples\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple functions within the same service\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Get started**

## Hello MediaConvert<a name="example_mediaconvert_Hello_section"></a>

The following code example shows how to get started using AWS Elemental MediaConvert\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/MediaConvert#code-examples)\. 
  

```
using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;

namespace MediaConvertActions;

public static class HelloMediaConvert
{
    static async Task Main(string[] args)
    {
        // Create the client using the default profile.
        var mediaConvertClient = new AmazonMediaConvertClient();

        Console.WriteLine($"Hello AWS Elemental MediaConvert! Your MediaConvert Endpoints are:");
        Console.WriteLine();

        // You can use await and any of the async methods to get a response.
        // Let's get the MediaConvert endpoints.
        var response = await mediaConvertClient.DescribeEndpointsAsync(
            new DescribeEndpointsRequest()
            );

        foreach (var endPoint in response.Endpoints)
        {
            Console.WriteLine($"\tEndPoint: {endPoint.Url}");
            Console.WriteLine();
        }
    }
}
```
+  For API details, see [DescribeEndpoints](https://docs.aws.amazon.com/goto/DotNetSDKV3/mediaconvert-2017-08-29/DescribeEndpoints) in *AWS SDK for \.NET API Reference*\. 

**Topics**
+ [Actions](#actions)

## Actions<a name="actions"></a>

### Create a transcoding job<a name="mediaconvert_CreateJob_csharp_topic"></a>

The following code example shows how to create an AWS Elemental MediaConvert transcoding job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/MediaConvert#code-examples)\. 
Get the endpoint and set up the client\.  

```
        // MediaConvert role Amazon Resource Name (ARN). 
        // For information on creating this role, see
        // https://docs.aws.amazon.com/mediaconvert/latest/ug/creating-the-iam-role-in-mediaconvert-configured.html.
        var mediaConvertRole = _configuration["mediaConvertRoleARN"];

        // Include the file input and output locations in settings.json or settings.local.json.
        var fileInput = _configuration["fileInput"];
        var fileOutput = _configuration["fileOutput"];

        // Load the customer endpoint, if it is known.
        // When you know what your Region-specific endpoint is, set it here, or set it in your settings.local.json file.
        var mediaConvertEndpoint = _configuration["mediaConvertEndpoint"];

        Console.WriteLine("Welcome to the MediaConvert Create Job example.");
        // If you don't have the customer-specific endpoint, request it here.
        if (string.IsNullOrEmpty(mediaConvertEndpoint))
        {
            Console.WriteLine("Getting customer-specific MediaConvert endpoint.");
            AmazonMediaConvertClient client = new AmazonMediaConvertClient();
            DescribeEndpointsRequest describeRequest = new DescribeEndpointsRequest();
            DescribeEndpointsResponse describeResponse = await client.DescribeEndpointsAsync(describeRequest);
            mediaConvertEndpoint = describeResponse.Endpoints[0].Url;
        }
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Using endpoint {mediaConvertEndpoint}.");
        Console.WriteLine(new string('-', 80));
        // Because you have a service URL for MediaConvert, you don't
        // need to set RegionEndpoint. If you do, the ServiceURL will
        // be overwritten.
        AmazonMediaConvertConfig mcConfig = new AmazonMediaConvertConfig
        {
            ServiceURL = mediaConvertEndpoint,
        };

        AmazonMediaConvertClient mcClient = new AmazonMediaConvertClient(mcConfig);

        var wrapper = new MediaConvertWrapper(mcClient);
```
  

```
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Creating job for input file {fileInput}.");
        var jobId = await wrapper.CreateJob(mediaConvertRole!, fileInput!, fileOutput!);
        Console.WriteLine($"Created job with Job ID: {jobId}");
        Console.WriteLine(new string('-', 80));
```
Create the job using the wrapper method and return the job ID\.  

```
    /// <summary>
    /// Create a job to convert a media file.
    /// </summary>
    /// <param name="mediaConvertRole">The Amazon Resource Name (ARN) of the media convert role, as specified here:
    /// https://docs.aws.amazon.com/mediaconvert/latest/ug/creating-the-iam-role-in-mediaconvert-configured.html</param>
    /// <param name="fileInput">The Amazon Simple Storage Service (Amazon S3) location of the input media file.</param>
    /// <param name="fileOutput">The Amazon S3 location for the output media file.</param>
    /// <returns>The ID of the new job.</returns>
    public async Task<string> CreateJob(string mediaConvertRole, string fileInput,
        string fileOutput)
    {
        CreateJobRequest createJobRequest = new CreateJobRequest
        {
            Role = mediaConvertRole
        };

        createJobRequest.UserMetadata.Add("Customer", "Amazon");

        JobSettings jobSettings = new JobSettings
        {
            AdAvailOffset = 0,
            TimecodeConfig = new TimecodeConfig
            {
                Source = TimecodeSource.EMBEDDED
            }
        };
        createJobRequest.Settings = jobSettings;

        #region OutputGroup

        OutputGroup ofg = new OutputGroup
        {
            Name = "File Group",
            OutputGroupSettings = new OutputGroupSettings
            {
                Type = OutputGroupType.FILE_GROUP_SETTINGS,
                FileGroupSettings = new FileGroupSettings
                {
                    Destination = fileOutput
                }
            }
        };

        Output output = new Output
        {
            NameModifier = "_1"
        };

        #region VideoDescription

        VideoDescription vdes = new VideoDescription
        {
            ScalingBehavior = ScalingBehavior.DEFAULT,
            TimecodeInsertion = VideoTimecodeInsertion.DISABLED,
            AntiAlias = AntiAlias.ENABLED,
            Sharpness = 50,
            AfdSignaling = AfdSignaling.NONE,
            DropFrameTimecode = DropFrameTimecode.ENABLED,
            RespondToAfd = RespondToAfd.NONE,
            ColorMetadata = ColorMetadata.INSERT,
            CodecSettings = new VideoCodecSettings
            {
                Codec = VideoCodec.H_264
            }
        };
        output.VideoDescription = vdes;

        H264Settings h264 = new H264Settings
        {
            InterlaceMode = H264InterlaceMode.PROGRESSIVE,
            NumberReferenceFrames = 3,
            Syntax = H264Syntax.DEFAULT,
            Softness = 0,
            GopClosedCadence = 1,
            GopSize = 90,
            Slices = 1,
            GopBReference = H264GopBReference.DISABLED,
            SlowPal = H264SlowPal.DISABLED,
            SpatialAdaptiveQuantization = H264SpatialAdaptiveQuantization.ENABLED,
            TemporalAdaptiveQuantization = H264TemporalAdaptiveQuantization.ENABLED,
            FlickerAdaptiveQuantization = H264FlickerAdaptiveQuantization.DISABLED,
            EntropyEncoding = H264EntropyEncoding.CABAC,
            Bitrate = 5000000,
            FramerateControl = H264FramerateControl.SPECIFIED,
            RateControlMode = H264RateControlMode.CBR,
            CodecProfile = H264CodecProfile.MAIN,
            Telecine = H264Telecine.NONE,
            MinIInterval = 0,
            AdaptiveQuantization = H264AdaptiveQuantization.HIGH,
            CodecLevel = H264CodecLevel.AUTO,
            FieldEncoding = H264FieldEncoding.PAFF,
            SceneChangeDetect = H264SceneChangeDetect.ENABLED,
            QualityTuningLevel = H264QualityTuningLevel.SINGLE_PASS,
            FramerateConversionAlgorithm =
                H264FramerateConversionAlgorithm.DUPLICATE_DROP,
            UnregisteredSeiTimecode = H264UnregisteredSeiTimecode.DISABLED,
            GopSizeUnits = H264GopSizeUnits.FRAMES,
            ParControl = H264ParControl.SPECIFIED,
            NumberBFramesBetweenReferenceFrames = 2,
            RepeatPps = H264RepeatPps.DISABLED,
            FramerateNumerator = 30,
            FramerateDenominator = 1,
            ParNumerator = 1,
            ParDenominator = 1
        };
        output.VideoDescription.CodecSettings.H264Settings = h264;

        #endregion VideoDescription

        #region AudioDescription

        AudioDescription ades = new AudioDescription
        {
            LanguageCodeControl = AudioLanguageCodeControl.FOLLOW_INPUT,
            // This name matches one specified in the following Inputs.
            AudioSourceName = "Audio Selector 1",
            CodecSettings = new AudioCodecSettings
            {
                Codec = AudioCodec.AAC
            }
        };

        AacSettings aac = new AacSettings
        {
            AudioDescriptionBroadcasterMix = AacAudioDescriptionBroadcasterMix.NORMAL,
            RateControlMode = AacRateControlMode.CBR,
            CodecProfile = AacCodecProfile.LC,
            CodingMode = AacCodingMode.CODING_MODE_2_0,
            RawFormat = AacRawFormat.NONE,
            SampleRate = 48000,
            Specification = AacSpecification.MPEG4,
            Bitrate = 64000
        };
        ades.CodecSettings.AacSettings = aac;
        output.AudioDescriptions.Add(ades);

        #endregion AudioDescription

        #region Mp4 Container

        output.ContainerSettings = new ContainerSettings
        {
            Container = ContainerType.MP4
        };
        Mp4Settings mp4 = new Mp4Settings
        {
            CslgAtom = Mp4CslgAtom.INCLUDE,
            FreeSpaceBox = Mp4FreeSpaceBox.EXCLUDE,
            MoovPlacement = Mp4MoovPlacement.PROGRESSIVE_DOWNLOAD
        };
        output.ContainerSettings.Mp4Settings = mp4;

        #endregion Mp4 Container

        ofg.Outputs.Add(output);
        createJobRequest.Settings.OutputGroups.Add(ofg);

        #endregion OutputGroup

        #region Input

        Input input = new Input
        {
            FilterEnable = InputFilterEnable.AUTO,
            PsiControl = InputPsiControl.USE_PSI,
            FilterStrength = 0,
            DeblockFilter = InputDeblockFilter.DISABLED,
            DenoiseFilter = InputDenoiseFilter.DISABLED,
            TimecodeSource = InputTimecodeSource.EMBEDDED,
            FileInput = fileInput
        };

        AudioSelector audsel = new AudioSelector
        {
            Offset = 0,
            DefaultSelection = AudioDefaultSelection.NOT_DEFAULT,
            ProgramSelection = 1,
            SelectorType = AudioSelectorType.TRACK
        };
        audsel.Tracks.Add(1);
        input.AudioSelectors.Add("Audio Selector 1", audsel);

        input.VideoSelector = new VideoSelector
        {
            ColorSpace = ColorSpace.FOLLOW
        };

        createJobRequest.Settings.Inputs.Add(input);

        #endregion Input

        var jobId = "";
        try
        {
            CreateJobResponse createJobResponse =
                await _amazonMediaConvert.CreateJobAsync(createJobRequest);

            jobId = createJobResponse.Job.Id;
        }
        catch (BadRequestException bre)
        {
            // If the endpoint was bad.
            if (bre.Message.StartsWith("You must use the customer-"))
            {
                // The exception contains the correct endpoint; extract it.
                var mediaConvertEndpoint = bre.Message.Split('\'')[1];
                Console.WriteLine(
                    $"Request failed, please use endpoint {mediaConvertEndpoint}.");
            }
            else
                throw;
        }

        return jobId;
    }
```
+  For API details, see [CreateJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/mediaconvert-2017-08-29/CreateJob) in *AWS SDK for \.NET API Reference*\. 

### Get a transcoding job<a name="mediaconvert_GetJob_csharp_topic"></a>

The following code example shows how to get an AWS Elemental MediaConvert transcoding job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/MediaConvert#code-examples)\. 
Get the endpoint and set up the client\.  

```
        // MediaConvert role Amazon Resource Name (ARN). 
        // For information on creating this role, see
        // https://docs.aws.amazon.com/mediaconvert/latest/ug/creating-the-iam-role-in-mediaconvert-configured.html.
        var mediaConvertRole = _configuration["mediaConvertRoleARN"];

        // Include the file input and output locations in settings.json or settings.local.json.
        var fileInput = _configuration["fileInput"];
        var fileOutput = _configuration["fileOutput"];

        // Load the customer endpoint, if it is known.
        // When you know what your Region-specific endpoint is, set it here, or set it in your settings.local.json file.
        var mediaConvertEndpoint = _configuration["mediaConvertEndpoint"];

        Console.WriteLine("Welcome to the MediaConvert Create Job example.");
        // If you don't have the customer-specific endpoint, request it here.
        if (string.IsNullOrEmpty(mediaConvertEndpoint))
        {
            Console.WriteLine("Getting customer-specific MediaConvert endpoint.");
            AmazonMediaConvertClient client = new AmazonMediaConvertClient();
            DescribeEndpointsRequest describeRequest = new DescribeEndpointsRequest();
            DescribeEndpointsResponse describeResponse = await client.DescribeEndpointsAsync(describeRequest);
            mediaConvertEndpoint = describeResponse.Endpoints[0].Url;
        }
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Using endpoint {mediaConvertEndpoint}.");
        Console.WriteLine(new string('-', 80));
        // Because you have a service URL for MediaConvert, you don't
        // need to set RegionEndpoint. If you do, the ServiceURL will
        // be overwritten.
        AmazonMediaConvertConfig mcConfig = new AmazonMediaConvertConfig
        {
            ServiceURL = mediaConvertEndpoint,
        };

        AmazonMediaConvertClient mcClient = new AmazonMediaConvertClient(mcConfig);

        var wrapper = new MediaConvertWrapper(mcClient);
```
Get a job by its ID\.  

```
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Getting job information for Job ID {jobId}");
        var job = await wrapper.GetJobById(jobId);
        Console.WriteLine($"Job {job.Id} created on {job.CreatedAt:d} has status {job.Status}.");
        Console.WriteLine(new string('-', 80));
```
  

```
    /// <summary>
    /// Get the job information for a job by its ID.
    /// </summary>
    /// <param name="jobId">The ID of the job.</param>
    /// <returns>The Job object.</returns>
    public async Task<Job> GetJobById(string jobId)
    {
        var jobResponse = await _amazonMediaConvert.GetJobAsync(
                new GetJobRequest
                {
                    Id = jobId
                });

        return jobResponse.Job;
    }
```
+  For API details, see [GetJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/mediaconvert-2017-08-29/GetJob) in *AWS SDK for \.NET API Reference*\. 

### List transcoding jobs<a name="mediaconvert_ListJobs_csharp_topic"></a>

The following code example shows how to list AWS Elemental MediaConvert transcoding jobs\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/MediaConvert#code-examples)\. 
Get the endpoint and set up the client\.  

```
        // MediaConvert role Amazon Resource Name (ARN). 
        // For information on creating this role, see
        // https://docs.aws.amazon.com/mediaconvert/latest/ug/creating-the-iam-role-in-mediaconvert-configured.html.
        var mediaConvertRole = _configuration["mediaConvertRoleARN"];

        // Include the file input and output locations in settings.json or settings.local.json.
        var fileInput = _configuration["fileInput"];
        var fileOutput = _configuration["fileOutput"];

        // Load the customer endpoint, if it is known.
        // When you know what your Region-specific endpoint is, set it here, or set it in your settings.local.json file.
        var mediaConvertEndpoint = _configuration["mediaConvertEndpoint"];

        Console.WriteLine("Welcome to the MediaConvert Create Job example.");
        // If you don't have the customer-specific endpoint, request it here.
        if (string.IsNullOrEmpty(mediaConvertEndpoint))
        {
            Console.WriteLine("Getting customer-specific MediaConvert endpoint.");
            AmazonMediaConvertClient client = new AmazonMediaConvertClient();
            DescribeEndpointsRequest describeRequest = new DescribeEndpointsRequest();
            DescribeEndpointsResponse describeResponse = await client.DescribeEndpointsAsync(describeRequest);
            mediaConvertEndpoint = describeResponse.Endpoints[0].Url;
        }
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Using endpoint {mediaConvertEndpoint}.");
        Console.WriteLine(new string('-', 80));
        // Because you have a service URL for MediaConvert, you don't
        // need to set RegionEndpoint. If you do, the ServiceURL will
        // be overwritten.
        AmazonMediaConvertConfig mcConfig = new AmazonMediaConvertConfig
        {
            ServiceURL = mediaConvertEndpoint,
        };

        AmazonMediaConvertClient mcClient = new AmazonMediaConvertClient(mcConfig);

        var wrapper = new MediaConvertWrapper(mcClient);
```
List the jobs with a particular status\.  

```
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Listing all complete jobs.");
        var completeJobs = await wrapper.ListAllJobsByStatus(JobStatus.COMPLETE);
        completeJobs.ForEach(j =>
        {
            Console.WriteLine($"Job {j.Id} created on {j.CreatedAt:d} has status {j.Status}.");
        });
```
List the jobs using a paginator\.  

```
    /// <summary>
    /// List all of the jobs with a particular status using a paginator.
    /// </summary>
    /// <param name="status">The status to use when listing jobs.</param>
    /// <returns>The list of jobs matching the status.</returns>
    public async Task<List<Job>> ListAllJobsByStatus(JobStatus? status = null)
    {
        var returnedJobs = new List<Job>();

        var paginatedJobs = _amazonMediaConvert.Paginators.ListJobs(
                new ListJobsRequest
                {
                    Status = status
                });

        // Get the entire list using the paginator.
        await foreach (var job in paginatedJobs.Jobs)
        {
            returnedJobs.Add(job);
        }

        return returnedJobs;
    }
```
+  For API details, see [ListJobs](https://docs.aws.amazon.com/goto/DotNetSDKV3/mediaconvert-2017-08-29/ListJobs) in *AWS SDK for \.NET API Reference*\. 