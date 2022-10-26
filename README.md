### 基于VOSK的离线中文语音识别的.Net Core Server
---

## 基本
本地Asp.Net Core WebApi, 上传文件进行语音识别，自带中文小模型，可替换其他语言模型。

## Vosk
[Vosk说明](https://alphacephei.com/vosk/index.zh)

## 模型下载
[中文Text模型](https://alphacephei.com/vosk/models/vosk-model-small-cn-0.22.zip)

[通用Speaker模型](https://alphacephei.com/vosk/models/vosk-model-spk-0.4.zip)

## 代码参考
Service部分大量参考（抄自） https://github.com/DimQ1/vosk-http-server

## TBD
中文单词的断句，目前是两个单词间隔大于0.4秒断句，期待找到更合理的方法

## 其他用法
如使用其他模型，将下载的Model解压后拷贝到Release下的TextModel文件夹下;

或替换工程里的Textmodel文件夹，将内容设为始终复制。

CN的大模型需要较大内存。