### 基于VOSK离线语音识别的.Net Core Server
---
## Vosk
[Vosk说明](https://alphacephei.com/vosk/index.zh)

## 模型下载
[中文Text模型](https://alphacephei.com/vosk/models/vosk-model-small-cn-0.22.zip)

[通用Speaker模型](https://alphacephei.com/vosk/models/vosk-model-spk-0.4.zip)

## 代码参考

Service部分大量参考（抄自） https://github.com/DimQ1/vosk-http-server

## TBD
中文单词的断句

## 其他用法
如使用其他模型，将下载的Model解压后拷贝到Release下的TextModel文件夹下;

或替换工程里的Textmodel文件夹，将内容设为始终复制。

CN的大模型需要较大内存。