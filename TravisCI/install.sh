#! /bin/sh

#To Get the direct download link, download the editor in chrome with the dev tool sopen, then see what URL the executable come from
#https://download.unity3d.com/download_unity/5d30cf096e79/MacEditorInstaller/Unity-2017.1.1f1.pkg

#Abstract these so that multiple component can be downloaded easier
BASE_URL=https://download.unity3d.com/download_unity/
HASH=5d30cf096e79
VERSION=2017.1.1f1

getFileName() {
    echo "${UNITY_DOWNLOAD_CACHE}/`basename "$1"`"
}

download() {
    file=$1
    url="$BASE_URL/$HASH/$file"
    filePath=$(getFileName $file)
    fileName=`basename "$file"`

    if [ ! -e $filePath ] ; then
        echo "Downloading $filePath from $url: "
        curl --retry 5 -o "$filePath" "$url"
    else
        echo "$fileName exists in cache. Skipping download."
    fi
}

install() {
    package=$1
    filePath=$(getFileName $package)

    download "$package"

    echo "Installing $filePath"
    sudo installer -dumplog -package "$filePath" -target /
}

install "MacEditorInstaller/Unity-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-iOS-Support-for-Editor-$VERSION.pkg"