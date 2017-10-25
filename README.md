# NLP-n-gram_and_TokenExtractor
In the first step a token extractor is implemented by DFA in order to extract words, digits and sentences from HTML files and gather data as dataset. Then, the dataset is used in n-gram.
This project is implemented in Microsoft Visual Studio in windows form and C# language.

It is considered to extract text from HTML files. This action is done in preprocessing step in a way that any tag is removed in body section of HTML source code. Then sentences based on simple rules and a DFA model are extracted. Then extracted sentences are used in the second processing step. accuracy of sentences from new html file is verified and an average accuracy is shown in each sentence. In this step bigram and trigram are used for verifying sentences from new text.

For further information, please read [wiki](https://github.com/mortezamg63/NLP-n-gram_and_TokenExtractor/wiki/Preprocessing--%5C--Extracting-Tokens) section of this project
