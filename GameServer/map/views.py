from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
from django.views.decorators.csrf import csrf_exempt
from django.middleware.csrf import get_token
import json
from django.contrib.auth.decorators import user_passes_test

# Create your views here.

@csrf_exempt
@user_passes_test(lambda u: u.is_superuser)
def getNewUsersInfo(request):
    with open("authentication/connectedUsers.json", "r") as jsonFile:
        jsonObject = json.load(jsonFile)
    with open("authentication/connectedUsers.json", "w") as jsonFile:
        jsonFile.write('{"NewConnectedUsers": [], "NewDisconnectedUsers": []}')
    return JsonResponse(jsonObject)


@csrf_exempt
@user_passes_test(lambda u: u.is_superuser)
def getNewTransformsInfo(request):
    with open("map/updateTransform.json", "r") as jsonFile:
        jsonObject = json.load(jsonFile)
    with open("map/updateTransform.json", "w") as jsonFile:
        jsonFile.write('{"items":[]}')
    return JsonResponse(jsonObject)


@csrf_exempt
def updateTransform(request):
    with open("map/updateTransform.json", "r") as jsonFile:
        jsonObject = json.load(jsonFile)
        jsonObject['items'].append({'username' : str(request.user), 'transform' : request.POST})
    with open("map/updateTransform.json", "w") as jsonFile:
        json.dump(jsonObject, jsonFile)
    return HttpResponse("OK")
