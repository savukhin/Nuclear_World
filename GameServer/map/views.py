from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
from django.views.decorators.csrf import csrf_exempt
from django.middleware.csrf import get_token
import json

# Create your views here.

@csrf_exempt
def getNewUsersInfo(request):
    with open("authentication/connectedUsers.json", "r") as jsonFile:
        jsonObject = json.load(jsonFile)
    with open("authentication/connectedUsers.json", "w") as jsonFile:
        jsonFile.write('{"NewConnectedUsers": [], "NewDisconnectedUsers": []}')
    return JsonResponse(jsonObject)
