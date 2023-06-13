# Amazon Transcribe examples using AWS SDK for \.NET<a name="csharp_transcribe_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon Transcribe\.

*Actions* are code excerpts from larger programs and must be run in context\. While actions show you how to call individual service functions, you can see actions in context in their related scenarios and cross\-service examples\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple functions within the same service\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#actions)

## Actions<a name="actions"></a>

### Create a custom vocabulary<a name="transcribe_CreateVocabulary_csharp_topic"></a>

The following code example shows how to create a custom Amazon Transcribe vocabulary\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Create a custom vocabulary using a list of phrases. Custom vocabularies
    /// improve transcription accuracy for one or more specific words.
    /// </summary>
    /// <param name="languageCode">The language code of the vocabulary.</param>
    /// <param name="phrases">Phrases to use in the vocabulary.</param>
    /// <param name="vocabularyName">Name for the vocabulary.</param>
    /// <returns>The state of the custom vocabulary.</returns>
    public async Task<VocabularyState> CreateCustomVocabulary(LanguageCode languageCode,
        List<string> phrases, string vocabularyName)
    {
        var response = await _amazonTranscribeService.CreateVocabularyAsync(
            new CreateVocabularyRequest
            {
                LanguageCode = languageCode,
                Phrases = phrases,
                VocabularyName = vocabularyName
            });
        return response.VocabularyState;
    }
```
+  For API details, see [CreateVocabulary](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/CreateVocabulary) in *AWS SDK for \.NET API Reference*\. 

### Delete a custom vocabulary<a name="transcribe_DeleteVocabulary_csharp_topic"></a>

The following code example shows how to delete a custom Amazon Transcribe vocabulary\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Delete an existing custom vocabulary.
    /// </summary>
    /// <param name="vocabularyName">Name of the vocabulary to delete.</param>
    /// <returns>True if successful.</returns>
    public async Task<bool> DeleteCustomVocabulary(string vocabularyName)
    {
        var response = await _amazonTranscribeService.DeleteVocabularyAsync(
            new DeleteVocabularyRequest
            {
                VocabularyName = vocabularyName
            });
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
```
+  For API details, see [DeleteVocabulary](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/DeleteVocabulary) in *AWS SDK for \.NET API Reference*\. 

### Delete a medical transcription job<a name="transcribe_DeleteMedicalTranscriptionJob_csharp_topic"></a>

The following code example shows how to delete an Amazon Transcribe Medical transcription job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Delete a medical transcription job. Also deletes the transcript associated with the job.
    /// </summary>
    /// <param name="jobName">Name of the medical transcription job to delete.</param>
    /// <returns>True if successful.</returns>
    public async Task<bool> DeleteMedicalTranscriptionJob(string jobName)
    {
        var response = await _amazonTranscribeService.DeleteMedicalTranscriptionJobAsync(
            new DeleteMedicalTranscriptionJobRequest()
            {
                MedicalTranscriptionJobName = jobName
            });
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
```
+  For API details, see [DeleteMedicalTranscriptionJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/DeleteMedicalTranscriptionJob) in *AWS SDK for \.NET API Reference*\. 

### Delete a transcription job<a name="transcribe_DeleteTranscriptionJob_csharp_topic"></a>

The following code example shows how to delete an Amazon Transcribe transcription job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Delete a transcription job. Also deletes the transcript associated with the job.
    /// </summary>
    /// <param name="jobName">Name of the transcription job to delete.</param>
    /// <returns>True if successful.</returns>
    public async Task<bool> DeleteTranscriptionJob(string jobName)
    {
        var response = await _amazonTranscribeService.DeleteTranscriptionJobAsync(
            new DeleteTranscriptionJobRequest()
            {
                TranscriptionJobName = jobName
            });
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
```
+  For API details, see [DeleteTranscriptionJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/DeleteTranscriptionJob) in *AWS SDK for \.NET API Reference*\. 

### Get a custom vocabulary<a name="transcribe_GetVocabulary_csharp_topic"></a>

The following code example shows how to get a custom Amazon Transcribe vocabulary\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Get information about a custom vocabulary.
    /// </summary>
    /// <param name="vocabularyName">Name of the vocabulary.</param>
    /// <returns>The state of the custom vocabulary.</returns>
    public async Task<VocabularyState> GetCustomVocabulary(string vocabularyName)
    {
        var response = await _amazonTranscribeService.GetVocabularyAsync(
            new GetVocabularyRequest()
            {
                VocabularyName = vocabularyName
            });
        return response.VocabularyState;
    }
```
+  For API details, see [GetVocabulary](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/GetVocabulary) in *AWS SDK for \.NET API Reference*\. 

### Get a transcription job<a name="transcribe_GetTranscriptionJob_csharp_topic"></a>

The following code example shows how to get an Amazon Transcribe transcription job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Get details about a transcription job.
    /// </summary>
    /// <param name="jobName">A unique name for the transcription job.</param>
    /// <returns>A TranscriptionJob instance with information on the requested job.</returns>
    public async Task<TranscriptionJob> GetTranscriptionJob(string jobName)
    {
        var response = await _amazonTranscribeService.GetTranscriptionJobAsync(
            new GetTranscriptionJobRequest()
            {
                TranscriptionJobName = jobName
            });
        return response.TranscriptionJob;
    }
```
+  For API details, see [GetTranscriptionJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/GetTranscriptionJob) in *AWS SDK for \.NET API Reference*\. 

### List custom vocabularies<a name="transcribe_ListVocabularies_csharp_topic"></a>

The following code example shows how to list custom Amazon Transcribe vocabularies\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// List custom vocabularies for the current account. Optionally specify a name
    /// filter and a specific state to filter the vocabularies list.
    /// </summary>
    /// <param name="nameContains">Optional string the vocabulary name must contain.</param>
    /// <param name="stateEquals">Optional state of the vocabulary.</param>
    /// <returns>List of information about the vocabularies.</returns>
    public async Task<List<VocabularyInfo>> ListCustomVocabularies(string? nameContains = null,
        VocabularyState? stateEquals = null)
    {
        var response = await _amazonTranscribeService.ListVocabulariesAsync(
            new ListVocabulariesRequest()
            {
                NameContains = nameContains,
                StateEquals = stateEquals
            });
        return response.Vocabularies;
    }
```
+  For API details, see [ListVocabularies](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/ListVocabularies) in *AWS SDK for \.NET API Reference*\. 

### List medical transcription jobs<a name="transcribe_ListMedicalTranscriptionJobs_csharp_topic"></a>

The following code example shows how to list Amazon Transcribe Medical transcription jobs\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// List medical transcription jobs, optionally with a name filter.
    /// </summary>
    /// <param name="jobNameContains">Optional name filter for the medical transcription jobs.</param>
    /// <returns>A list of summaries about medical transcription jobs.</returns>
    public async Task<List<MedicalTranscriptionJobSummary>> ListMedicalTranscriptionJobs(
        string? jobNameContains = null)
    {
        var response = await _amazonTranscribeService.ListMedicalTranscriptionJobsAsync(
            new ListMedicalTranscriptionJobsRequest()
            {
                JobNameContains = jobNameContains
            });
        return response.MedicalTranscriptionJobSummaries;
    }
```
+  For API details, see [ListMedicalTranscriptionJobs](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/ListMedicalTranscriptionJobs) in *AWS SDK for \.NET API Reference*\. 

### List transcription jobs<a name="transcribe_ListTranscriptionJobs_csharp_topic"></a>

The following code example shows how to list Amazon Transcribe transcription jobs\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// List transcription jobs, optionally with a name filter.
    /// </summary>
    /// <param name="jobNameContains">Optional name filter for the transcription jobs.</param>
    /// <returns>A list of transcription job summaries.</returns>
    public async Task<List<TranscriptionJobSummary>> ListTranscriptionJobs(string? jobNameContains = null)
    {
        var response = await _amazonTranscribeService.ListTranscriptionJobsAsync(
            new ListTranscriptionJobsRequest()
            {
                JobNameContains = jobNameContains
            });
        return response.TranscriptionJobSummaries;
    }
```
+  For API details, see [ListTranscriptionJobs](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/ListTranscriptionJobs) in *AWS SDK for \.NET API Reference*\. 

### Start a medical transcription job<a name="transcribe_StartMedicalTranscriptionJob_csharp_topic"></a>

The following code example shows how to start an Amazon Transcribe Medical transcription job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Start a medical transcription job for a media file. This method returns
    /// as soon as the job is started.
    /// </summary>
    /// <param name="jobName">A unique name for the medical transcription job.</param>
    /// <param name="mediaFileUri">The URI of the media file, typically an Amazon S3 location.</param>
    /// <param name="mediaFormat">The format of the media file.</param>
    /// <param name="outputBucketName">Location for the output, typically an Amazon S3 location.</param>
    /// <param name="transcriptionType">Conversation or dictation transcription type.</param>
    /// <returns>A MedicalTransactionJob instance with information on the new job.</returns>
    public async Task<MedicalTranscriptionJob> StartMedicalTranscriptionJob(
        string jobName, string mediaFileUri,
        MediaFormat mediaFormat, string outputBucketName, Amazon.TranscribeService.Type transcriptionType)
    {
        var response = await _amazonTranscribeService.StartMedicalTranscriptionJobAsync(
            new StartMedicalTranscriptionJobRequest()
            {
                MedicalTranscriptionJobName = jobName,
                Media = new Media()
                {
                    MediaFileUri = mediaFileUri
                },
                MediaFormat = mediaFormat,
                LanguageCode =
                    LanguageCode
                        .EnUS, // The value must be en-US for medical transcriptions.
                OutputBucketName = outputBucketName,
                OutputKey =
                    jobName, // The value is a key used to fetch the output of the transcription.
                Specialty = Specialty.PRIMARYCARE, // The value PRIMARYCARE must be set.
                Type = transcriptionType
            });
        return response.MedicalTranscriptionJob;
    }
```
+  For API details, see [StartMedicalTranscriptionJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/StartMedicalTranscriptionJob) in *AWS SDK for \.NET API Reference*\. 

### Start a transcription job<a name="transcribe_StartTranscriptionJob_csharp_topic"></a>

The following code example shows how to start an Amazon Transcribe transcription job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Start a transcription job for a media file. This method returns
    /// as soon as the job is started.
    /// </summary>
    /// <param name="jobName">A unique name for the transcription job.</param>
    /// <param name="mediaFileUri">The URI of the media file, typically an Amazon S3 location.</param>
    /// <param name="mediaFormat">The format of the media file.</param>
    /// <param name="languageCode">The language code of the media file, such as en-US.</param>
    /// <param name="vocabularyName">Optional name of a custom vocabulary.</param>
    /// <returns>A TranscriptionJob instance with information on the new job.</returns>
    public async Task<TranscriptionJob> StartTranscriptionJob(string jobName, string mediaFileUri,
        MediaFormat mediaFormat, LanguageCode languageCode, string? vocabularyName)
    {
        var response = await _amazonTranscribeService.StartTranscriptionJobAsync(
            new StartTranscriptionJobRequest()
            {
                TranscriptionJobName = jobName,
                Media = new Media()
                {
                    MediaFileUri = mediaFileUri
                },
                MediaFormat = mediaFormat,
                LanguageCode = languageCode,
                Settings = vocabularyName != null ? new Settings()
                {
                    VocabularyName = vocabularyName
                } : null
            });
        return response.TranscriptionJob;
    }
```
+  For API details, see [StartTranscriptionJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/StartTranscriptionJob) in *AWS SDK for \.NET API Reference*\. 

### Update a custom vocabulary<a name="transcribe_UpdateVocabulary_csharp_topic"></a>

The following code example shows how to update a custom Amazon Transcribe vocabulary\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Transcribe#code-examples)\. 
  

```
    /// <summary>
    /// Update a custom vocabulary with new values. Update overwrites all existing information.
    /// </summary>
    /// <param name="languageCode">The language code of the vocabulary.</param>
    /// <param name="phrases">Phrases to use in the vocabulary.</param>
    /// <param name="vocabularyName">Name for the vocabulary.</param>
    /// <returns>The state of the custom vocabulary.</returns>
    public async Task<VocabularyState> UpdateCustomVocabulary(LanguageCode languageCode,
        List<string> phrases, string vocabularyName)
    {
        var response = await _amazonTranscribeService.UpdateVocabularyAsync(
            new UpdateVocabularyRequest()
            {
                LanguageCode = languageCode,
                Phrases = phrases,
                VocabularyName = vocabularyName
            });
        return response.VocabularyState;
    }
```
+  For API details, see [UpdateVocabulary](https://docs.aws.amazon.com/goto/DotNetSDKV3/transcribe-2017-10-26/UpdateVocabulary) in *AWS SDK for \.NET API Reference*\. 