#! /bin/sh

UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

echo "Unity path: ${UNITY_PATH}"

activateLicense() {
    echo "Activate Unity"

    ${UNITY_PATH} \
        -serial ${UNITY_SERIAL} \
        -username ${UNITY_USER} \
        -password ${UNITY_PWD} \
        -batchmode \
        -noUpm \
        -quit
}

 # prepareBuilds() {
     # echo "Preparing building"

     # mkdir ${BUILD_PATH}
     # echo "Created directory: ${BUILD_PATH}"
 # }

# buildiOS() {
    # echo "Building ${UNITY_PROJECT_NAME} for iOS"

    # ${UNITY_PATH} \
        # -batchmode \
        # -silent-crashes \
        # -logFile "${TRAVIS_BUILD_DIR}/unity.build.ios.log" \
        # -projectPath "${TRAVIS_BUILD_DIR}/${UNITY_PROJECT_NAME}" \
        # -executeMethod Syng.Builds.Build \
        # -syngBuildPath "${BUILD_PATH}/iOS" \
        # -quit

    # rc1=$?
    # echo "Build logs (iOS)"
    # cat "${TRAVIS_BUILD_DIR}/unity.build.ios.log"

    # # exit if build failed
    # if [ $rc1 -ne 0 ]; then { echo "Build failed"; exit $rc1; } fi
# }

buildPackage() {
	echo "Project Path: ${TRAVIS_BUILD_DIR}"
	ls "${TRAVIS_BUILD_DIR}"
	
	${UNITY_PATH} \
         -batchmode \
         -silent-crashes \
		 -stackTraceLogType "Script Only" \
         -logFile "${TRAVIS_BUILD_DIR}/unity.build.package.log" \
         -projectPath "${TRAVIS_BUILD_DIR}" \
         -executeMethod DebugOnlyPackageBuilder.Build \
         -quit

     rc1=$?
     echo "Build logs (Package)"
     cat "${TRAVIS_BUILD_DIR}/unity.build.package.log"

     # exit if build failed
     if [ $rc1 -ne 0 ]; then { echo "Build failed"; exit $rc1; } fi
}

returnLicense() {
    echo "Return license"

    ${UNITY_PATH} \
        -batchmode \
        -returnlicense \
        -quit
}

echo "${TRAVIS_BUILD_DIR} Contents:"
ls "${TRAVIS_BUILD_DIR}"

activateLicense
#prepareBuilds
#buildiOS
buildPackage
returnLicense

echo "${TRAVIS_BUILD_DIR} Contents:"
ls "${TRAVIS_BUILD_DIR}"

echo ""
echo "${TRAVIS_BUILD_DIR}/Assets Contents:"
ls "${TRAVIS_BUILD_DIR}/Assets"

exit 0